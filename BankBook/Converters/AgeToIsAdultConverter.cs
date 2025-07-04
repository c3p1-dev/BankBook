﻿using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace BankBook.Converters
{
    public class AgeToIsAdultConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int age)
            {
                return age >= 18;
            }
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
