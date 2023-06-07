using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestAppBack.DataAccess.Utils
{
    /// <summary>
    /// Класс для парсинга типа бд по строке
    /// </summary>
    public static class DbTypeParser
    {
        private static IEnumerable<string> _types;

        /// <summary>
        /// Парсинг типа БД
        /// </summary>
        /// <param name="dbType">Тип БД вне зависимости от регистра</param>
        /// <returns>Тип БД из enum</returns>
        public static DbType Parse(string dbType)
        {
            if(_types is null)
                _types = Enum.GetValues(typeof(DbType)).Cast<DbType>().Select((p) => Enum.GetName<DbType>(p)!.ToLower().Trim()).ToList();

            // приведение запрашиваемого типа в формат для поиска
            string formatedIncomeType = dbType.Trim().ToLower();

            // поиск запрашиваемого типа
            int incomeTypeIndexInEnum = -1;
            for (int i = 0; i < _types.Count(); i++)
            {
                if (_types.ElementAt(i) == formatedIncomeType) incomeTypeIndexInEnum = i;
            }

            if (incomeTypeIndexInEnum == -1) throw new KeyNotFoundException("Данный тип БД не был найден");

            return (DbType)incomeTypeIndexInEnum;
        }
    }
}
