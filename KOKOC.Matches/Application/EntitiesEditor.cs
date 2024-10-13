using System.Reflection;

namespace KOKOC.Matches.Application
{
    public class EntitiesEditor<TEntity> where TEntity : class
    {
        private readonly IEnumerable<PropertyInfo> _properties;
        public EntitiesEditor()
        {
            _properties = typeof(TEntity).GetProperties()
                .Where(x => x.PropertyType.IsPrimitive);
        }
        public void Edit(TEntity baseEntity, TEntity dataSource)
        {
            foreach (var property in _properties)
            {
                var baseProperty = property.GetValue(baseEntity);
                var sourceProperty = property.GetValue(dataSource);
                var comparerType = typeof(EqualityComparer<>).MakeGenericType(property.PropertyType);
                var equalsMethod = comparerType.GetMethod("Equals");
                var comparer = comparerType.GetProperty("Default")!.GetValue(null, null);

                if ((bool)equalsMethod.Invoke(comparer, [baseEntity, sourceProperty])!)
                {
                    property.SetValue(baseEntity, sourceProperty, null);
                }
            }
        }
    }
}
