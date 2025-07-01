using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainManager.Models
{
    public class DomainListViewModel
    {
        public List<Domain> Domains { get; set; } = new List<Domain>();

        public string? SearchTerm { get; set; }

        public DomainStatus? StatusFilter { get; set; }

        public int TotalCount { get; set; }
    }
}