using Kitchen.Models;

namespace Kitchen.ViewModel
{
    public class AllStaffWithPagesViewModel
    {
        public List<AllStaffViewModel> allStaff; 
        public int TotalPages {  get; set; }
        public int PageSize { get; set; } = 3;
        public int CurrentPage { get; set; } = 1;

    }
}
