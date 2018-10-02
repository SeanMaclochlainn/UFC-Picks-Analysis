﻿using FightData.Domain.Finders;
using FightData.Domain.Entities;
using System.Collections.Generic;
using System.Net;
using FightData.Domain;

namespace FightDataProcessor
{
    //public class ExhibitionWebpagesCollector
    //{
    //    private ExhibitionCollectingUi exhibitionUi;
    //    private WebpageFinder webpageFinder;

    //    public ExhibitionWebpagesCollector() : this(new AppUi()) { }

    //    public ExhibitionWebpagesCollector(AppUi ui)
    //    {
    //        this.exhibitionUi = new ExhibitionCollectingUi(ui);
    //        this.webpageFinder = new WebpageFinder();
    //    }

    //    public List<Webpage> CollectExhibitionWebpages()
    //    {
    //        List<Website> websites = webpageFinder.GetAllWebsites();
    //        List<Webpage> webpages = new List<Webpage>();
    //        foreach (var website in websites)
    //        {
    //            string websiteUrl = exhibitionUi.GetWebsiteUrl(website);
    //            if (SkipWebsite(websiteUrl))
    //                continue;
    //            else
    //            {
    //                webpages.Add(new Webpage(new FightPicksContext())
    //                {
    //                    Url = websiteUrl,
    //                    Website = website,
    //                    Data = DownloadWebpage(websiteUrl)
    //                });
    //            }
    //        }
    //        return webpages;
    //    }

    //    private string DownloadWebpage(string url)
    //    {
    //        string webPage = "";
    //        using (WebClient client = new WebClient())
    //        {
    //            webPage = client.DownloadString(url);
    //        }
    //        return webPage;
    //    }

    //    private bool SkipWebsite(string url)
    //    {
    //        return string.IsNullOrEmpty(url);
    //    }
    //}
}


//private bool WebpageAlreadyExists(int websiteId)
//{
//    return webpageFinder.WebpageExists(currentEvent.Id, websiteId);
//}

//private void PopulateWebpageData(string url, Website website)
//{
//    string webpageData = DownloadWebpage(url);
//    if (WebpageAlreadyExists(website.Id))
//    {
//        UpdateExistingWebpage(webpageData, url, website.Id);
//    }
//    Webpage webpage = new Webpage(url, website, currentEvent, webpageData);
//    webpage.AddWebpage();
//}

//private void UpdateExistingWebpage(string webpageData, string webpageUrl, int websiteId)
//{
//    Webpage webpage = webpageFinder.GetWebpage(currentEvent.Id, websiteId);
//    webpage.UpdateWebpage(webpageData, webpageUrl);
//}

//HttpClient client = new HttpClient();
//    foreach (var website in websites)
//    {
//        Webpage webpage = new Webpage();
//        Console.WriteLine("Enter {0} url (Enter to skip website)", website.WebsiteName.ToString());
//        string websiteUrl = Console.ReadLine();
//        if (!string.IsNullOrEmpty(websiteUrl))
//        {
//            webpage.Url = websiteUrl;
//            webpage.Event = eventObj;
//            webpage.Website = website;
//            Task<HttpResponseMessage> result = client.GetAsync(websiteUrl);
//            while (!result.IsCompleted)
//            {

//            }
//            HttpResponseMessage result2 = result.Result;
//            HttpContent content = result2.Content;
//            Task<string> contentstr = content.ReadAsStringAsync();
//            while (!contentstr.IsCompleted)
//            {

//            }
//            webpage.Data = contentstr.Result;
//            Webpage existingWebpage = dataUtilities.FindWebpage(webpage.Event.Id, webpage.Website.Id);
//            if (existingWebpage != null)
//            {
//                webpage = dataUtilities.UpdateWebpage(webpage);
//            }
//            else
//            {
//                dataUtilities.AddWebpage(webpage);
//            }
//        }
//    }
//}




//public void DeleteAllPicks()
//{
//    dataUtilities.DeleteAllPicks();
//}

//Task<HttpResponseMessage> result = client.GetAsync(websiteUrl);
//            while (!result.IsCompleted)
//            {

//            }
//            HttpResponseMessage result2 = result.Result;
//HttpContent content = result2.Content;
//Task<string> contentstr = content.ReadAsStringAsync();
//            while (!contentstr.IsCompleted)
//            {

//            }
//            webpage.Data = contentstr.Result;


