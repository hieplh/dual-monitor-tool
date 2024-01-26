﻿#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015  Gerald Evans
// 
// Dual Monitor Tools is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion

namespace DMT.Library.Html
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Represents an HTML element (tag) and allows access to its attributes
	/// </summary>
	public class HtmlElement
	{
		string _elementText;
		List<Tuple<string, string>> _attributes;

		/// <summary>
		/// Initialises a new instance of the <see cref="HtmlElement" /> class.
		/// </summary>
		/// <param name="allElementText">Full text of element including opening and closing angle brackets</param>
		public HtmlElement(string allElementText)
		{
			if (allElementText == null)
			{
				// shouldn't get here, but jic
				allElementText = string.Empty;
			}

			// strip of the leading "<" or "</"
 			// and trailing ">" "/>"
			int offset = 0;
			int len = allElementText.Length;
			if (len >= 1 && allElementText[len - 1] == '>')
			{
				len--;
				if (len >= 1 && allElementText[len - 1] == '/')
				{
					len--;
				}
			}

			if (len >= 1 && allElementText[0] == '<')
			{
				offset++;
				len--;
				if (len >= 1 && allElementText[1] == '/')
				{
					offset++;
				}
			}

			_elementText = allElementText.Substring(offset, len).Trim();
			_attributes = null;	// we parse these only if required
		}

		/// <summary>
		/// Gets the element name
		/// </summary>
		/// <returns>Element name</returns>
		public string GetElementName()
		{
			// returns the first word in the element text
			// We allow '-' as aften used in pseudo html
			// also convert to lower case to simplify comparison
			StringBuilder sb = new StringBuilder();
			foreach (char ch in _elementText)
			{
				if (IsElementNameChar(ch))
				{
					sb.Append(char.ToLower(ch));
				}
				else
				{
					break;
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Gets a list of all of the attributes
		/// </summary>
		/// <returns>List of attributes</returns>
		public List<Tuple<string, string>> GetAllAttributes()
		{
			return GetAttributes();
		}

		/// <summary>
		/// Gets a particular attribute
		/// </summary>
		/// <param name="attributeName">Name of attribute to get</param>
		/// <returns>Attribute value, or null if missing</returns>
		public string GetAttribute(string attributeName)
		{
			List<Tuple<string, string>> attributes = GetAttributes();
			foreach (Tuple<string, string> pair in attributes)
			{
				if (pair.Item1 == attributeName)
				{
					return pair.Item2;
				}
			}

			return null;
		}

		List<Tuple<string, string>> GetAttributes()
		{
			if (_attributes == null)
			{
				_attributes = new List<Tuple<string, string>>();

				int index = 0;

				// skip over element name
				while (index < _elementText.Length && IsElementNameChar(_elementText[index]))
				{
					index++;
				}

				while (index < _elementText.Length)
				{
					// skip over spaces
					while (index < _elementText.Length && char.IsWhiteSpace(_elementText[index]))
					{
						index++;
					}

					// pick up attribute name (in lower case to simplify comparison)
					int attributeNameIndex = index;
					while (index < _elementText.Length && IsAttributeNameChar(_elementText[index]))
					{
						index++;
					}

					string attributeName = _elementText.Substring(attributeNameIndex, index - attributeNameIndex).ToLower();

					if (attributeName.Length == 0)
					{
						// looks like we have rubbish here,
						// so pick up and dispose of any garbage we find here
						while (index < _elementText.Length && !char.IsWhiteSpace(_elementText[index]) && _elementText[index] != '=')
						{
							index++;
						}
					}

					// skip over spaces
					while (index < _elementText.Length && char.IsWhiteSpace(_elementText[index]))
					{
						index++;
					}

					StringBuilder attributeValue = new StringBuilder();
					if (index < _elementText.Length && _elementText[index] == '=')
					{
						// skip over the space
						index++;

						// skip over spaces
						while (index < _elementText.Length && char.IsWhiteSpace(_elementText[index]))
						{
							index++;
						}

						// need to handle quoted strings
						char quoteChar = '\0';
						if (index < _elementText.Length && IsQuote(_elementText[index]))
						{
							quoteChar = _elementText[index];
							index++;
						}

						// TODO: shoudl really handle escapes as well
						while (index < _elementText.Length && _elementText[index] != quoteChar)
						{
							if (quoteChar == '\0' && char.IsWhiteSpace(_elementText[index]))
							{
								break;
							}

							attributeValue.Append(_elementText[index]);
							index++;
						}

						if (index < _elementText.Length && _elementText[index] == quoteChar)
						{
							index++;
						}
					}

					if (attributeName.Length > 0)
					{
						// TODO: need to unencode HTML attribute value
						_attributes.Add(new Tuple<string, string>(attributeName, attributeValue.ToString()));
					}
				}
			}

			return _attributes;
		}

		bool IsElementNameChar(char ch)
		{
			return char.IsLetterOrDigit(ch) || ch == '-';
		}

		bool IsAttributeNameChar(char ch)
		{
			return char.IsLetterOrDigit(ch) || ch == '-';
		}

		bool IsQuote(char ch)
		{
			return ch == '\'' || ch == '\"';
		}
	}
}
