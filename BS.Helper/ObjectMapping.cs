using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Helper
{
    public class ObjectMapping
    {
        public static void MapProperties<TSource, TDestination>(TSource source, TDestination destination)
        {
            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);

                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
        }

        public static void MapProperties<TSource, TDestination>(TSource source, TDestination destination, string mapProperties)
        {
            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();

            var propertiesToMap = mapProperties.Split(',');

            foreach (var propertyName in propertiesToMap)
            {
                var sourceProperty = sourceProperties.FirstOrDefault(p => p.Name == propertyName);
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == propertyName);

                if (sourceProperty != null && destinationProperty != null && destinationProperty.CanWrite)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
        }
    }
}
