using FitPortal.Models.Domain;
using FitPortal.Models.DTO;

namespace FitPortal.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            this.eventList = new List<EventInformation>();
            this.categories = new List<PostCategory>();
        }
        public void AddPost(EventInformation eventInformation)
        {
            EventInformation temp = new EventInformation();
            temp = eventInformation;
            this.eventList.Add(temp);
        }
        public void AddCategory(PostCategory category)
        {
            PostCategory temp = new PostCategory();
            temp = category;
            this.categories.Add(temp);
        }
        public List<EventInformation> eventList { get; set; }
        public List<PostCategory> categories { get; set; }
    }
}
