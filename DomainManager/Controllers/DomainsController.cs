using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using DomainManager.Models;

namespace DomainManager.Controllers
{
    public class DomainsController : Controller
    {
        // 模拟数据存储（后续会用数据库替代）
        private static List<Domain> _domains = new List<Domain>
        {
            new Domain
            {
                Id = 1,
                Name = "example.com",
                Status = DomainStatus.Available,
                RegisterDate = new DateTime(2024, 1, 15),
                ExpiryDate = new DateTime(2025, 1, 15),
                Notes = "主要业务网站"
            },
            new Domain
            {
                Id = 2,
                Name = "mysite.org",
                Status = DomainStatus.Expired,
                RegisterDate = new DateTime(2023, 6, 20),
                ExpiryDate = new DateTime(2024, 6, 20),
                Notes = "测试网站"
            },
            new Domain
            {
                Id = 3,
                Name = "testdomain.net",
                Status = DomainStatus.Expiring,
                RegisterDate = new DateTime(2023, 12, 1),
                ExpiryDate = new DateTime(2025, 12, 1),
                Notes = "开发环境"
            }
        };

        // 显示域名列表
        public IActionResult Index(string searchTerm, DomainStatus? statusFilter)
        {
            var domains = _domains.AsQueryable();

            // 按名称搜索
            if (!string.IsNullOrEmpty(searchTerm))
            {
                domains = domains.Where(d => d.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // 按状态筛选
            if (statusFilter.HasValue)
            {
                domains = domains.Where(d => d.Status == statusFilter.Value);
            }

            var viewModel = new DomainListViewModel
            {
                Domains = domains.ToList(),
                SearchTerm = searchTerm,
                StatusFilter = statusFilter,
                TotalCount = domains.Count()
            };

            return View(viewModel);
        }

        // 显示添加域名表单
        public IActionResult Create()
        {
            return View(new Domain
            {
                RegisterDate = DateTime.Today,
                ExpiryDate = DateTime.Today.AddYears(1)
            });
        }

        // 处理添加域名表单提交
        [HttpPost]
        public IActionResult Create(Domain domain)
        {
            if (ModelState.IsValid)
            {
                domain.Id = _domains.Max(d => d.Id) + 1;
                _domains.Add(domain);
                TempData["SuccessMessage"] = $"域名 {domain.Name} 添加成功！";
                return RedirectToAction(nameof(Index));
            }
            return View(domain);
        }

        // 删除域名
        public IActionResult Delete(int id)
        {
            var domain = _domains.FirstOrDefault(d => d.Id == id);
            if (domain != null)
            {
                _domains.Remove(domain);
                TempData["SuccessMessage"] = $"域名 {domain.Name} 删除成功！";
            }
            else
            {
                TempData["ErrorMessage"] = "域名不存在！";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}