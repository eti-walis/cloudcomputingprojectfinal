using DAL;
using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using DALj;

namespace BLj
{
    class CheckDrugs
    {
        public string checkDrugs(string medicine, int id, DateTime start, DateTime end)
        {
            ImplementDAL idal = new ImplementDAL();
            string ndc = (from x in idal.getAllMedicines() where x.Name == medicine select x.MedecienId‏).First();
            string siteContent = string.Empty;
            string url = "https://rxnav.nlm.nih.gov/REST/rxcui?idtype=NDC&id=" + ndc;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())  // Go query google
            using (Stream responseStream = response.GetResponseStream())               // Load the response stream
            using (StreamReader streamReader = new StreamReader(responseStream))       // Load the stream reader to read the response
            {
                siteContent = streamReader.ReadToEnd(); // Read the entire response and store it in the siteContent variable
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(siteContent);
            XmlNodeList xnList = doc.SelectNodes("/rxnormdata/idGroup");
            string rxcui = string.Empty;
            string tmp2 = string.Empty;
            if (xnList[0]["rxnormId"] != null)//the drug has rxcui code
            {
                rxcui = xnList[0]["rxnormId"].InnerText;
                //find all the other drugs the patient use
                List<string> rxcuis = new List<string>();
                List<Prescription> allPrescription = (from x in idal.getAllPrescriptions() where x.Id == id select x).ToList();
                foreach (Prescription p in allPrescription)
                {
                    if (!((p.StartData < start && p.EndData < start) || (p.StartData > end)))//משתמש בתרופה אחרת במקביל לתרופה החדשה
                    {
                        Medicine m = (from x in idal.getAllMedicines() where x.Name == p.MedicineName select x).First();
                        url = "https://rxnav.nlm.nih.gov/REST/rxcui?idtype=NDC&id=" + m.MedecienId‏;
                        request = (HttpWebRequest)WebRequest.Create(url);
                        request.AutomaticDecompression = DecompressionMethods.GZip;

                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())  // Go query google
                        using (Stream responseStream = response.GetResponseStream())               // Load the response stream
                        using (StreamReader streamReader = new StreamReader(responseStream))       // Load the stream reader to read the response
                        {
                            siteContent = streamReader.ReadToEnd();
                        }
                        doc.LoadXml(siteContent);
                        xnList = doc.SelectNodes("/rxnormdata/idGroup");
                        if (xnList[0]["rxnormId"] != null)
                            rxcuis.Add(xnList[0]["rxnormId"].InnerText);

                    }
                }
                string tmp = string.Empty;
                for (int i = 0; i < rxcuis.Count; i++)
                {
                    tmp = tmp + "+" + rxcuis[i];

                };
                url = "https://rxnav.nlm.nih.gov/REST/interaction/list.json?rxcuis=" + rxcui + tmp;
                request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())  // Go query google
                using (Stream responseStream = response.GetResponseStream())               // Load the response stream
                using (StreamReader streamReader = new StreamReader(responseStream))       // Load the stream reader to read the response
                {
                    siteContent = streamReader.ReadToEnd();
                }

                Root DetailsTree = JsonConvert.DeserializeObject<Root>(siteContent);

                foreach (FullInteractionType fullI in DetailsTree.fullInteractionTypeGroup[0].fullInteractionType)
                {
                    tmp2 = tmp2 + fullI.interactionPair[0].description + "\n";
                }

                foreach (FullInteractionType fullI in DetailsTree.fullInteractionTypeGroup[0].fullInteractionType)
                {
                    if (fullI.interactionPair[0].severity == "high")
                        throw new Exception("The severity is high");
                }

            }


            return tmp2;
        }

    }
    public class UserInput
    {
        public List<string> sources { get; set; }
        public List<string> rxcuis { get; set; }
    }

    public class MinConcept
    {
        public string rxcui { get; set; }
        public string name { get; set; }
        public string tty { get; set; }
    }

    public class MinConceptItem
    {
        public string rxcui { get; set; }
        public string name { get; set; }
        public string tty { get; set; }
    }

    public class SourceConceptItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class InteractionConcept
    {
        public MinConceptItem minConceptItem { get; set; }
        public SourceConceptItem sourceConceptItem { get; set; }
    }

    public class InteractionPair
    {
        public List<InteractionConcept> interactionConcept { get; set; }
        public string severity { get; set; }
        public string description { get; set; }
    }

    public class FullInteractionType
    {
        public string comment { get; set; }
        public List<MinConcept> minConcept { get; set; }
        public List<InteractionPair> interactionPair { get; set; }
    }

    public class FullInteractionTypeGroup
    {
        public string sourceDisclaimer { get; set; }
        public string sourceName { get; set; }
        public List<FullInteractionType> fullInteractionType { get; set; }
    }

    public class Root
    {
        public string nlmDisclaimer { get; set; }
        public UserInput userInput { get; set; }
        public List<FullInteractionTypeGroup> fullInteractionTypeGroup { get; set; }

    }

}



