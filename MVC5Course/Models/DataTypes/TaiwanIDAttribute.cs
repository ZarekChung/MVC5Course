using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.DataTypes
{
	public class TaiwanIDAttribute : DataTypeAttribute
	{
		public TaiwanIDAttribute() : base(DataType.Text)
		{
		}

		public override bool IsValid(object value)
		{
			if(value == null)
			{
				return true;
			}

			string str = (string)value;

			if(str.Contains("product"))
			{
				return true;
			}
			else
			{
				return false;
			}

			//return base.IsValid(value);
		}

	}
}