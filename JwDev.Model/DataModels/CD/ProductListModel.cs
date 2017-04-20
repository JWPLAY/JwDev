﻿using JwDev.Model.Attributes;
using JwDev.Model.Enums;

namespace JwDev.Model.Codes
{
	public class ProductListModel
	{
		[CustomProperty(ColumnType = ColumnTypeEnum.Integer, HorzAlignment = HorzAlignmentEnum.Center, Width = 50)]
		public int ROW_NO { get; set; }
		public int PRODUCT_ID { get; set; }
		public string PRODUCT_CODE { get; set; }
		public string PRODUCT_NAME { get; set; }
		public string PRODUCT_TYPE { get; set; }
		public string CATEGORY { get; set; }
		public string USE_YN { get; set; }
		public decimal SALE_PRICE { get; set; }
		public decimal COST_PRICE { get; set; }
	}
}
