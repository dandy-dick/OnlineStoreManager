using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineStoreManager.Models;

namespace OnlineStoreManager.Infracstructure
{
    public enum PageName
    { 
        Summary = 1,
        Order = 2,
        Inventory = 3,
        Product = 4,
        Admin = 5,
    }

    public enum TabName
    {
        // tong quan
        Revenue = 1,
        Spending = 2,

        // quan ly don hang
        Order = 8,
        PaidOrder = 4,
        ExportedOrder = 5,
        CompletedOrder = 6,
        CanceledOrder = 7,
        
        // don hang nhap kho
        ImportOrder = 9,
        WaitingImportOrd = 10,
        ImportedOrd = 11,
        CanceledImportOrd = 12,

        // quan ly ton kho
        Stock = 14,
        Warehouse = 15,
        // quan ly san pham
        Product = 16,
        Category = 17,
        Supplier = 18,
        // 
        Account= 19
    }

    public interface IPageMaster 
    {
        public string GetPageName(PageName? page = null);
        public string GetTabName(TabName? tab = null);

        public void SetTabName(TabName tab);
    }
    public class PageMaster: IPageMaster
    {
        public TabName TabName { get; set; }
        public PageName PageName { get; set; }

        public PageMaster()
        {
        }

        public void SetTabName(TabName tab)
        {
            this.TabName = tab;
            this.PageName = GetTabPageName(tab);
        }

        public PageName GetTabPageName(TabName tab)
        {
            switch (tab)
            {
                case TabName.Revenue:
                case TabName.Spending:
                    return PageName.Summary;
                case TabName.Order:
                case TabName.PaidOrder:
                case TabName.ExportedOrder:
                case TabName.CompletedOrder:
                case TabName.CanceledOrder:
                    return PageName.Order;
                case TabName.ImportOrder:
                case TabName.WaitingImportOrd:
                case TabName.ImportedOrd:
                case TabName.CanceledImportOrd:
                case TabName.Stock:
                case TabName.Warehouse:
                    return PageName.Inventory;
                case TabName.Product:
                case TabName.Category:
                case TabName.Supplier:
                    return PageName.Product;
                case TabName.Account:
                default:
                    return PageName.Admin;
            }
        }

        public string GetTabName(TabName? tab = null)
        {
            tab ??= this.TabName;
            return tab switch
            {
                TabName.Revenue => "Báo cáo doanh thu",
                TabName.Spending => "Báo cáo chi tiêu",

                TabName.Order => "Đơn hàng ecommerce",
                TabName.PaidOrder => "Đã thanh toán",
                TabName.ExportedOrder => "Đã xuất kho",
                TabName.CompletedOrder => "Hoàn tất",
                TabName.CanceledOrder => "Bị hủy",

                TabName.ImportOrder => "Đơn hàng nhập kho",
                TabName.WaitingImportOrd => "Đang xử lí",
                TabName.ImportedOrd => "Đã nhập kho",
                TabName.CanceledImportOrd => "Bị hủy",

                TabName.Stock => "Sản phẩm tồn kho",
                TabName.Warehouse => "Kho hàng",
                TabName.Product => "Sản phẩm đang bán",
                TabName.Category => "Danh mục sản phẩm",
                TabName.Supplier => "Nhà cung cấp",
                TabName.Account => "Quản lý tài khoản",
                _ => "",
            };
        }

        public string GetPageName(PageName? page = null)
        {
            page ??= this.PageName;
            return page switch
            {
                PageName.Summary => "Tổng quan",
                PageName.Order => "Quản lý đơn hàng",
                PageName.Inventory => "Quản lý kho",
                PageName.Product => "Quản lý sản phẩm",
                PageName.Admin => "Admin",
                _ => "",
            };
        }
    }
}
