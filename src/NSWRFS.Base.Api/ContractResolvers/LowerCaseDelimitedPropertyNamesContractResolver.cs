// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LowerCaseDelimitedPropertyNamesContractResolver.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the LowerCaseDelimitedPropertyNamesContractResolver type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.Base.Api.ContractResolvers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Globalization;

    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The lower case delimited property names contract resovler.
    /// </summary>
    public class LowerCaseDelimitedPropertyNamesContractResolver : DefaultContractResolver
    {
        private readonly char delimiter;

        /// <summary>
        /// Initializes a new instance of the <see cref="LowerCaseDelimitedPropertyNamesContractResolver"/> class.
        /// </summary>
        /// <param name="delimiter">
        /// The delimiter.
        /// </param>
        public LowerCaseDelimitedPropertyNamesContractResolver(char delimiter)
            : base(true)
        {
            this.delimiter = delimiter;
        }

        /// <summary>
        /// The resolve property name.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToDelimitedString(this.delimiter);
        }
    }

    /// <summary>
    /// The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The to delimited string.
        /// </summary>
        /// <param name="string">
        /// The string.
        /// </param>
        /// <param name="delimiter">
        /// The delimiter.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDelimitedString(this string @string, char delimiter)
        {
            var camelCaseString = @string.ToCamelCaseString();
            return new string(InsertDelimiterBeforeCaps(camelCaseString, delimiter).ToArray());
        }

        /// <summary>
        /// The to camel case string.
        /// </summary>
        /// <param name="string">
        /// The string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToCamelCaseString(this string @string)
        {
            if (string.IsNullOrEmpty(@string) || !char.IsUpper(@string[0]))
            {
                return @string;
            }
            string lowerCasedFirstChar =
                char.ToLower(@string[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            if (@string.Length > 1)
            {
                lowerCasedFirstChar = lowerCasedFirstChar + @string.Substring(1);
            }
            return lowerCasedFirstChar;
        }

        /// <summary>
        /// The insert delimiter before caps.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="delimiter">
        /// The delimiter.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private static IEnumerable<char> InsertDelimiterBeforeCaps(IEnumerable input, char delimiter)
        {
            bool lastCharWasUppper = false;
            foreach (char c in input)
            {
                if (char.IsUpper(c))
                {
                    if (!lastCharWasUppper)
                    {
                        yield return delimiter;
                        lastCharWasUppper = true;
                    }
                    yield return char.ToLower(c);
                    continue;
                }

                yield return c;
                lastCharWasUppper = false;
            }
        }
    }
}