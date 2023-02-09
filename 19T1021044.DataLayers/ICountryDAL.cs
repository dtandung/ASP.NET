using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021044.DomainModels;

namespace _19T1021044.DataLayers
{
	/// <summary>
	/// Định nghĩa các chức năng xử lý dự liệu cho quốc gia
	/// </summary>
	public interface ICountryDAL
	{
		/// <summary>
		/// Lấy danh sách tất cả các quốc gia
		/// </summary>
		/// <returns></returns>
		IList<Country> List();
	}
}
