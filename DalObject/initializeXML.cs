using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Dal
{
    public class XmlTools
    {
        static string dir = @"";
        static XmlTools()
        {
            if (dir != "" && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        #region SaveLoadWithXElement
        /// <summary>
        /// Writing to file using XElement
        /// </summary>
        /// <param name="rootElem">file root</param>
        /// <param name="filePath">file path</param>
        public static void SaveListToXMLElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dir + filePath);
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        /// <summary>
        /// Uploading using XElement
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns>list of elements in file</returns>
        public static XElement LoadListFromXMLElement(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    return XElement.Load(dir + filePath);
                }
                else
                {
                    XElement rootElem = new XElement(dir + filePath);
                    rootElem.Save(dir + filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion

        #region SaveLoadWithXMLSerializer
        /// <summary>
        /// Writing to file using XMLSerializer
        /// </summary>
        /// <typeparam name="T">type of elements in file</typeparam>
        /// <param name="list">list of elements to save</param>
        /// <param name="filePath">file path</param>
        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(dir + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        /// <summary>
        /// Uploading using XMLSerializer
        /// </summary>
        /// <typeparam name="T">type of elements</typeparam>
        /// <param name="filePath">file path</param>
        /// <returns>list of elements</returns>
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dir + filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
    }
    class initializeXML
    {

        static public void loadXML()
        {

            string dronesPath = @"DronesXml.xml"; //XElement
            string stationsPath = @"StationsXml.xml"; //XMLSerializer
            string customersPath = @"CustomersXml.xml"; //XMLSerializer
            string parcelsPath = @"ParcelsXml.xml"; //XMLSerializer

            XElement dronesRootElem = XmlTools.LoadListFromXMLElement(dronesPath);
            dronesRootElem.RemoveAll();
            foreach (DO.Drone drone in DataSource.Drones)
            {
                XElement droneElem = new XElement("Drone", new XElement("Id", drone.Id),
                                      new XElement("Model", drone.Model),
                                      new XElement("MaxWeight", drone.MaxWeight.ToString()));
                dronesRootElem.Add(droneElem);
            }

            XmlTools.SaveListToXMLElement(dronesRootElem, dronesPath);

            XmlTools.SaveListToXMLSerializer(DataSource.Customers, customersPath);
            XmlTools.SaveListToXMLSerializer(DataSource.Parcels, parcelsPath);
            XmlTools.SaveListToXMLSerializer(DataSource.Stations, stationsPath);
        }
    }
}
