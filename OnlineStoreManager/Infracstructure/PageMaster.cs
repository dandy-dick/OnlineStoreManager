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
        Revenue = 1003,
        Spending = 1004,
        // quan ly don hang
        Order = 1009,
        // quan ly ton kho
        ImportOrder = 1005,
        Stock = 1006,
        Warehouse = 1007,
        // quan ly san pham
        Product = 1000,
        Category = 1001,
        Supplier = 1002,
        // 
        Account= 1008
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
                    return PageName.Order;
                case TabName.ImportOrder:
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
            tab = tab ?? this.TabName;
            switch (tab)
            {
                case TabName.Revenue:
                    return "Báo cáo doanh thu";
                case TabName.Spending:
                    return "Báo cáo chi tiêu";
                case TabName.Order:
                    return "Đơn hàng";
                case TabName.ImportOrder:
                    return "Đơn hàng nhập kho";
                case TabName.Stock:
                    return "Sản phẩm tồn kho";
                case TabName.Warehouse:
                    return "Kho hàng";
                case TabName.Product:
                    return "Sản phẩm";
                case TabName.Category:
                    return "Danh mục sản phẩm";
                case TabName.Supplier:
                    return "Nhà cung cấp";
                case TabName.Account:
                    return "Quản lý tài khoản";
                default:
                    return "";
            }
        }

        public string GetPageName(PageName? page = null)
        {
            page = page ?? this.PageName;
            switch (page)
            {
                case PageName.Summary:
                    return "Tổng quan";
                case PageName.Order:
                    return "Quản lý đơn hàng";
                case PageName.Inventory:
                    return "Quản lý kho";
                case PageName.Product:
                    return "Quản lý sản phẩm";
                case PageName.Admin:
                    return "Admin";
                default:
                    return "";
            }
        }
    }
}
