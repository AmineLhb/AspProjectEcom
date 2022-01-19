using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen_ASP.Net.Models
{
    public class ViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
        public IEnumerable<BestOffer> BestOffers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Image> images { get; set; }
        public List<Message> Messages { get; set; }
        public List<ProfileInfo> ProfileInfos { get; set; }
        public List<HomeInfo> HomeInfos { get; set; }
    }
    public class ProfileInfo
    {
        public string UserName { get; set; }
        public string Path { get; set; }
        public string Text { get; set; }
        public int Buyer_id { get; set; }
        public int Id { get; set; }
    }
    public class HomeInfo
    {
        public Product Product { get; set; }
        public List<Image> Images { get; set; }
    }
    public class BestOffer
    {
        public Product Product { get; set; }
        public Image Images { get; set; }
    }

}