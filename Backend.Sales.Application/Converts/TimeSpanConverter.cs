using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System;
using System.Globalization;

namespace Backend.Sales.Application.Converters
{
    public class TimeOnlyConverter : DefaultTypeConverter
    {
        private const string TimeFormat = "HH:mm:ss"; // Use uppercase "HH" for 24-hour format

        public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text is null)
            {
                return default(TimeOnly); // Or throw an exception if necessary
            }

            if (TimeOnly.TryParseExact(text, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var timeOnly))
            {
                return timeOnly;
            }

            throw new InvalidCastException($"'{text}' is not a valid TimeOnly value.");
        }

        public override string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is TimeOnly timeOnly)
            {
                return timeOnly.ToString(TimeFormat, CultureInfo.InvariantCulture);
            }

            throw new InvalidCastException($"'{value}' is not a valid TimeOnly value.");
        }
    }
}