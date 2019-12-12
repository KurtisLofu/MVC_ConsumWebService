using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Model1
    {
        private static string url = "https://localhost:44391/api/";

        #region CONTACTES
        public List<contacte> GetContactes()
        {
            List<contacte> c = (List<contacte>)MakeRequest(url + "contactesTot/", null, "GET", "application/json", typeof(List<contacte>));
            return c;
        }

        public contacte GetContacteById(int id)
        {
            contacte c = (contacte)MakeRequest(url + "contacteTot/" + id + "/", null, "GET", "application/json", typeof(contacte));
            return c;
        }
        public List<contacte> GetContactesByName(string nom)
        {
            List<contacte> lista = (List<contacte>) MakeRequest(url + "contactesTot/" + nom + "/", null, "GET", "application/json", typeof(List<contacte>));
            return lista;
        }

        public contacte InsertContacte(contacte c2Add)
        {
            contacte c = (contacte)MakeRequest(url + "contacte/", c2Add, "POST", "application/json", typeof(contacte));
            return c;
        }

        public contacte UpdateContacte(contacte cUpdate)
        {
            contacte c = (contacte)MakeRequest(url + "contacte/" + cUpdate.contacteId + "/", cUpdate, "PUT", "application/json", typeof(contacte));
            return c;
        }

        public void DeleteContacte(int id)
        {
            MakeRequest(url + "contacteTot/" + id + "/", null, "DELETE", null, typeof(void));
        }
        #endregion

        #region TELEFONS    

        public List<contacte> GetContactesByTelefon(string num)
        {
            List<contacte> lista = (List<contacte>)MakeRequest(url + "telefonC/" + num + "/", null, "GET", "application/json", typeof(List<contacte>));
            return lista;
        }

        public telefon UpdateTelefon(telefon tUpdate)
        {
            telefon t = (telefon)MakeRequest(url + "telefon/" + tUpdate.telId + "/", tUpdate, "PUT", "application/json", typeof(telefon));
            return t;
        }

        public telefon InsertTelefon(telefon t2Add)
        {
            telefon c = (telefon)MakeRequest(url + "telefon/", t2Add, "POST", "application/json", typeof(telefon));
            return c;
        }

        public void DeleteTelefon(int id)
        {
            MakeRequest(url + "telefon/" + id + "/", null, "DELETE", null, typeof(void));
        }
        #endregion


        #region EMAILS

        public List<contacte> GetContactesByEmail(string email)
        {
            List<contacte> lista = (List<contacte>)MakeRequest(url + "emailC/" + email + "/", null, "GET", "application/json", typeof(List<contacte>));
            return lista;
        }

        public email UpdateEmail(email mUpdate)
        {
            email t = (email)MakeRequest(url + "email/" + mUpdate.emailId + "/", mUpdate, "PUT", "application/json", typeof(email));
            return t;
        }

        public email InsertEmail(email m2Add)
        {
            email c = (email)MakeRequest(url + "email/", m2Add, "POST", "application/json", typeof(email));
            return c;
        }

        public void DeleteEmail(int id)
        {
            MakeRequest(url + "email/" + id + "/", null, "DELETE", null, typeof(void));
        }
        #endregion

        public object MakeRequest(string requestUrl, object JSONRequest, string JSONmethod, string JSONContentType, Type JSONResponseType)
    //  requestUrl: Url completa del Web Service, amb l'opció sol·licitada
    //  JSONrequest: objecte que se li passa en el body (només per a POST/PUT)
    //  JSONmethod: "GET"/"POST"/"PUT"/"DELETE"
    //  JSONContentType: "application/json" en els casos que el Web Service torni objectes
    //  JSONRensponseType:  tipus d'objecte que torna el Web Service (typeof(tipus))
    {
        try
        {
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest; //WebRequest WR = WebRequest.Create(requestUrl);   
            string sb = JsonConvert.SerializeObject(JSONRequest);
            request.Method = JSONmethod;  // "GET"/"POST"/"PUT"/"DELETE";  

            if (JSONmethod != "GET")
            {
                request.ContentType = JSONContentType; // "application/json";   
                Byte[] bt = Encoding.UTF8.GetBytes(sb);
                Stream st = request.GetRequestStream();
                st.Write(bt, 0, bt.Length);
                st.Close();
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                Stream stream1 = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream1);
                string strsb = sr.ReadToEnd();
                object objResponse = JsonConvert.DeserializeObject(strsb, JSONResponseType);
                return objResponse;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}
}
