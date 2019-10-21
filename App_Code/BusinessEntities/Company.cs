using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using ePayHrms.Connection;



namespace ePayHrms.Company
{
    /// <summary>
    /// Summary description for Company
    /// </summary>
    public class Company
    {
        public Company()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _CompanyId;
        private int _BranchCompanyId;
        private string _CompanyCode;
        private string _CompanyName;
        private string _AddressLine1;
        private string _AddressLine2;
        private string _City;
        private string _ZipCode;
        private string _PhoneNo;
        private string _FaxNo;
        private string _EmailId;
        private string _CountryName;
        private string _StateName;
        private string _UserId;
        private string _Password;
        private string _AlterEmailId;
        public string result;
        private string _PFCode;
        private string _ESICode;
        private string _PFno;
        private string _ESIno;
        private DateTime _Start_date;
        private DateTime _End_date;
        private string _temp1;
        private string _temp2;
        private char _role;
        private char _status;
        private int _count;

        public HttpServerUtility LoadFile;
        private SqlConnection _Connection;
        ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
        public string XMLCountryName;
        public bool bContinue = false;

        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }

        public int count
        {
            get { return _count; }
            set { _count = value; }
        }


        public int BranchCompanyId
        {
            get { return _BranchCompanyId; }
            set { _BranchCompanyId = value; }
        }

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set { _CompanyCode = value; }
        }

        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }

        public string AddressLine1
        {
            get { return _AddressLine1; }
            set { _AddressLine1 = value; }
        }

        public string AddressLine2
        {
            get { return _AddressLine2; }
            set { _AddressLine2 = value; }
        }

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }

        public string PhoneNo
        {
            get { return _PhoneNo; }
            set { _PhoneNo = value; }
        }

        public string FaxNo
        {
            get { return _FaxNo; }
            set { _FaxNo = value; }
        }

        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }

        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }

        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
        }

        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public string Alternate_EmailId
        {
            get { return _AlterEmailId; }
            set { _AlterEmailId = value; }
        }

        public DateTime Start_date
        {
            get { return _Start_date; }
            set { _Start_date = value; }
        }

        public DateTime End_date
        {
            get { return _End_date; }
            set { _End_date = value; }
        }

        public string PFno
        {
            get { return _PFno; }
            set { _PFno = value; }
        }

        public string ESIno
        {
            get { return _ESIno; }
            set { _ESIno = value; }
        }

        public string PFCode
        {
            get { return _PFCode; }
            set { _PFCode = value; }
        }

        public string ESICode
        {
            get { return _ESICode; }
            set { _ESICode = value; }
        }

        public string temp1
        {
            get { return _temp1; }
            set { _temp1 = value; }
        }
        public string temp2
        {
            get { return _temp2; }
            set { _temp2 = value; }
        }

        public char role
        {
            get { return _role; }
            set { _role = value; }
        }

        public char status
        {
            get { return _status; }
            set { _status = value; }
        }
        //public string fn_WriteXMLCompany(Company company)
        //{
        //    int _iCnt;
        //    int _Iter;
        //    int _RootIter;
        //    XmlDocument _XDoc = new XmlDocument();
        //    _XDoc.Load(@"D:\FinalNewMicro\Hrms_Common\Company.xml");
        //    XmlNodeList _objNodeList = _XDoc.GetElementsByTagName("Root");
        //    if (null == _objNodeList)
        //        return null;
        //    for (_RootIter = 0; _RootIter < _objNodeList.Count; _RootIter++)
        //    {
        //        XmlNode _objNode = _objNodeList.Item(_RootIter);
        //        if (null == _objNode)
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            XmlNodeList _objCompanyList = _objNode.ChildNodes;
        //            if (null == _objCompanyList)
        //                return null;
        //            for (_iCnt = 0; _iCnt < _objCompanyList.Count; _iCnt++)
        //            {
        //                XmlNode _objCompany = _objCompanyList.Item(_iCnt);
        //                if (null == _objCompany)
        //                    continue;
        //                for (_Iter = 0; _Iter < _objCompany.Attributes.Count; _Iter++)
        //                {
        //                    if (_objCompany.Attributes[_Iter].Name.CompareTo("CompanyCode") == 0)
        //                    {
        //                        if (company.CompanyCode.CompareTo(_objCompany.Attributes[4].Value) == 0)
        //                        {
        //                            bContinue = true;
        //                            result = "1";
        //                            continue;
        //                        }
        //                        if ((company.UserId.CompareTo(_objCompany.Attributes[1].Value) == 0) && (company.Password.CompareTo(_objCompany.Attributes[2].Value) == 0))
        //                        {
        //                            bContinue = true;
        //                            Update(company);
        //                            result = "1";
        //                            continue;
        //                        }
        //                    }
        //                }
        //            }
        //            if (false == bContinue)
        //            {
        //                XmlElement CompanyNode = _XDoc.CreateElement("Company");

        //                XmlAttribute Name = _XDoc.CreateAttribute("Name");
        //                XmlAttribute UserId = _XDoc.CreateAttribute("UserId");
        //                XmlAttribute Password = _XDoc.CreateAttribute("Password");
        //                XmlAttribute NavigateUrl = _XDoc.CreateAttribute("NavigateUrl");
        //                XmlAttribute CompanyCode = _XDoc.CreateAttribute("CompanyCode");

        //                Name.Value = "Admin";
        //                UserId.Value = company.UserId;
        //                Password.Value = company.Password;
        //                NavigateUrl.Value = "Hrms_Common/BranchIndex.aspx";
        //                CompanyCode.Value = company.CompanyCode;

        //                CompanyNode.SetAttributeNode(Name);
        //                CompanyNode.SetAttributeNode(UserId);
        //                CompanyNode.SetAttributeNode(Password);
        //                CompanyNode.SetAttributeNode(NavigateUrl);
        //                CompanyNode.SetAttributeNode(CompanyCode);

        //                _XDoc.DocumentElement.InsertAfter(CompanyNode, _XDoc.DocumentElement.LastChild);

        //                FileStream Xml = new FileStream(@"D:\FinalNewMicro\Hrms_Common\Company.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
        //                _XDoc.Save(Xml);
        //                Xml.Close();

        //                result = Update(company);
        //            }
        //        }
        //    }
        //    return result;
        //}


        //public string fn_WriteXMLBrachCompany(string _pCompanyCode, Company company)
        //{
        //    int _iCnt;
        //    int _Iter;
        //    int _IterNode;
        //    int _IterXMLNode;
        //    XmlDocument _XDoc = new XmlDocument();
        //    _XDoc.Load(@"D:\FinalNewMicro\Hrms_Common\Company.xml");
        //    XmlNodeList _objNodeList = _XDoc.GetElementsByTagName("Company");
        //    if (null == _objNodeList)
        //        return null;
        //    for (_iCnt = 0; _iCnt < _objNodeList.Count; _iCnt++)
        //    {
        //        XmlNode _objBaseNode = _objNodeList.Item(_iCnt);
        //        if (null == _objBaseNode)
        //            continue;
        //        for (_Iter = 0; _Iter < _objBaseNode.Attributes.Count; _Iter++)
        //        {
        //            if (_objBaseNode.Attributes[_Iter].Name.CompareTo("CompanyCode") == 0)
        //            {
        //                if (_pCompanyCode.CompareTo(_objBaseNode.Attributes[4].Value) == 0)
        //                {
        //                    if ((company.UserId.CompareTo(_objBaseNode.Attributes[1].Value) == 0) && (company.Password.CompareTo(_objBaseNode.Attributes[2].Value) == 0))
        //                    {
        //                        bContinue = true;
        //                        result = "1";
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        XmlNodeList _ObjChildNodeList = _objBaseNode.ChildNodes;
        //                        if (null == _ObjChildNodeList)
        //                        {
        //                            continue;
        //                        }
        //                        else
        //                        {
        //                            for (_IterNode = 0; _IterNode < _ObjChildNodeList.Count; _IterNode++)
        //                            {
        //                                XmlNode _objXMLNode = _ObjChildNodeList.Item(_IterNode);
        //                                if (null == _objXMLNode)
        //                                    continue;
        //                                for (_IterXMLNode = 0; _IterXMLNode < _objXMLNode.Attributes.Count; _IterXMLNode++)
        //                                {
        //                                    if (_objXMLNode.Attributes[_IterXMLNode].Name.CompareTo("CompanyCode") == 0)
        //                                    {
        //                                        if (company.CompanyCode.CompareTo(_objXMLNode.Attributes[4].Value) == 0)
        //                                        {
        //                                            bContinue = true;
        //                                            result = "1";
        //                                            continue;
        //                                        }
        //                                        if ((company.UserId.CompareTo(_objXMLNode.Attributes[1].Value) == 0) && (company.Password.CompareTo(_objXMLNode.Attributes[2].Value) == 0))
        //                                        {
        //                                            bContinue = true;
        //                                            result = "1";
        //                                            continue;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            if (false == bContinue)
        //                            {
        //                                XmlElement CompanyNode = _XDoc.CreateElement("Company");

        //                                XmlAttribute Name = _XDoc.CreateAttribute("Name");
        //                                XmlAttribute UserId = _XDoc.CreateAttribute("UserId");
        //                                XmlAttribute Password = _XDoc.CreateAttribute("Password");
        //                                XmlAttribute NavigateUrl = _XDoc.CreateAttribute("NavigateUrl");
        //                                XmlAttribute CompanyCode = _XDoc.CreateAttribute("CompanyCode");

        //                                Name.Value = "Admin";
        //                                UserId.Value = company.UserId;
        //                                Password.Value = company.Password;
        //                                NavigateUrl.Value = "Hrms_Common/BranchCompany.aspx";
        //                                CompanyCode.Value = company.CompanyCode;

        //                                CompanyNode.SetAttributeNode(Name);
        //                                CompanyNode.SetAttributeNode(UserId);
        //                                CompanyNode.SetAttributeNode(Password);
        //                                CompanyNode.SetAttributeNode(NavigateUrl);
        //                                CompanyNode.SetAttributeNode(CompanyCode);

        //                                _objBaseNode.PrependChild(CompanyNode);
        //                                _objBaseNode.InsertAfter(CompanyNode, _objBaseNode.LastChild);
        //                                FileStream Xml = new FileStream(@"D:\FinalNewMicro\Hrms_Common\Company.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
        //                                _XDoc.Save(Xml);
        //                                Xml.Close();

        //                                result = BranchUpdate(company);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}






        //public Company(string id, string ticker)
        //{
        //    this.CompanyCode = id;
        //    this.CompanyName = ticker;
        //}

        //public Collection<Country> fn_getXmlCountry()
        //{
        //    int _iCnt;
        //    int _Iter;
        //    Collection<Country> CountryList = new Collection<Country>();
        //    XmlDocument _XDoc = new XmlDocument();
        //    //_XDoc.Load(LoadFile.t.MapPath(@"../hrmm_Common/Country.xml"));
        //    _XDoc.Load(@"D:\FinalNewMicro\Hrms_Common\Country.xml");
        //    XmlNodeList _objNodeList = _XDoc.GetElementsByTagName("Country");
        //    if (null == _objNodeList)
        //        return null;
        //    for (_iCnt = 0; _iCnt < _objNodeList.Count; _iCnt++)
        //    {
        //        XmlNode _objBaseNode = _objNodeList.Item(_iCnt);
        //        if (null == _objBaseNode)
        //            continue;
        //        for (_Iter = 0; _Iter < _objBaseNode.Attributes.Count; _Iter++)
        //        {
        //            Country country = new Country();
        //            country.CountryName = _objBaseNode.Attributes[_Iter].Value;
        //            CountryList.Add(country);
        //        }
        //    }
        //    return CountryList;
        //}


        //public Collection<State> fn_getXmlState(string _SCounrtyName)
        //{
        //    int _iCnt;
        //    int _Iter;
        //    int _IterNode;
        //    int _IterXMLNode;
        //    Collection<State> StateList = new Collection<State>();
        //    XmlDocument _XDoc = new XmlDocument();
        //    _XDoc.Load(@"D:\FinalNewMicro\Hrms_Common\Country.xml");
        //    XmlNodeList _objNodeList = _XDoc.GetElementsByTagName("Country");
        //    if (null == _objNodeList)
        //        return null;
        //    for (_iCnt = 0; _iCnt < _objNodeList.Count; _iCnt++)
        //    {
        //        XmlNode _objBaseNode = _objNodeList.Item(_iCnt);
        //        if (null == _objBaseNode)
        //            continue;
        //        for (_Iter = 0; _Iter < _objBaseNode.Attributes.Count; _Iter++)
        //        {
        //            if (_objBaseNode.Attributes[_Iter].Name.CompareTo("Name") == 0)
        //            {
        //                if (_SCounrtyName.CompareTo(_objBaseNode.Attributes[0].Value) == 0)
        //                {
        //                    XmlNodeList _ObjChildNodeList = _objBaseNode.ChildNodes;
        //                    if (null == _ObjChildNodeList)
        //                    {
        //                        return StateList;
        //                    }
        //                    else
        //                    {
        //                        for (_IterNode = 0; _IterNode < _ObjChildNodeList.Count; _IterNode++)
        //                        {
        //                            XmlNode _objXMLNode = _ObjChildNodeList.Item(_IterNode);
        //                            if (null == _objXMLNode)
        //                                continue;
        //                            for (_IterXMLNode = 0; _IterXMLNode < _objXMLNode.Attributes.Count; _IterXMLNode++)
        //                            {
        //                                State state = new State();
        //                                state.StateName = _objXMLNode.Attributes[_IterXMLNode].Value;
        //                                StateList.Add(state);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return StateList;
        //}

        //public string fn_WriteXMLCountry(string _SCounrtyName)
        //{
        //    int _iCnt;
        //    int _Iter;
        //    XmlDocument _XDoc = new XmlDocument();
        //    _XDoc.Load(@"D:\FinalNewMicro\Hrms_Common\Country.xml");
        //    XmlNodeList _objNodeList = _XDoc.GetElementsByTagName("Country");
        //    if (null == _objNodeList)
        //        return null;
        //    for (_iCnt = 0; _iCnt < _objNodeList.Count; _iCnt++)
        //    {
        //        XmlNode _objBaseNode = _objNodeList.Item(_iCnt);
        //        if (null == _objBaseNode)
        //            continue;
        //        for (_Iter = 0; _Iter < _objBaseNode.Attributes.Count; _Iter++)
        //        {
        //            if (_objBaseNode.Attributes[_Iter].Name.CompareTo("Name") == 0)
        //            {
        //                if (_SCounrtyName.CompareTo(_objBaseNode.Attributes[0].Value) == 0)
        //                {
        //                    bContinue = true;
        //                    continue;
        //                }

        //            }
        //        }
        //    }
        //    if (false == bContinue)
        //    {
        //        XmlElement Country = _XDoc.CreateElement("Country");
        //        XmlAttribute Name = _XDoc.CreateAttribute("Name");
        //        Name.Value = _SCounrtyName;
        //        Country.SetAttributeNode(Name);
        //        _XDoc.DocumentElement.InsertAfter(Country, _XDoc.DocumentElement.LastChild);
        //        FileStream Xml = new FileStream(@"D:\FinalNewMicro\Hrms_Common\Country.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
        //        _XDoc.Save(Xml);
        //        Xml.Close();
        //        _SCounrtyName = _SCounrtyName;
        //    }
        //    return _SCounrtyName;
        //}

        //public string fn_WriteXMLState(string _SCountryName, string _SStateName)
        //{
        //    int _iCnt;
        //    int _Iter;
        //    int _IterNode;
        //    int _IterXMLNode;
        //    XmlDocument _XDoc = new XmlDocument();
        //    _XDoc.Load(@"D:\FinalNewMicro\Hrms_Common\Country.xml");
        //    XmlNodeList _objNodeList = _XDoc.GetElementsByTagName("Country");
        //    if (null == _objNodeList)
        //        return null;
        //    for (_iCnt = 0; _iCnt < _objNodeList.Count; _iCnt++)
        //    {
        //        XmlNode _objBaseNode = _objNodeList.Item(_iCnt);
        //        if (null == _objBaseNode)
        //            continue;
        //        for (_Iter = 0; _Iter < _objBaseNode.Attributes.Count; _Iter++)
        //        {
        //            if (_objBaseNode.Attributes[_Iter].Name.CompareTo("Name") == 0)
        //            {
        //                if (_SCountryName.CompareTo(_objBaseNode.Attributes[0].Value) == 0)
        //                {
        //                    XmlNodeList _ObjChildNodeList = _objBaseNode.ChildNodes;
        //                    if (null == _ObjChildNodeList)
        //                    {
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        for (_IterNode = 0; _IterNode < _ObjChildNodeList.Count; _IterNode++)
        //                        {
        //                            XmlNode _objXMLNode = _ObjChildNodeList.Item(_IterNode);
        //                            if (null == _objXMLNode)
        //                                continue;
        //                            for (_IterXMLNode = 0; _IterXMLNode < _objXMLNode.Attributes.Count; _IterXMLNode++)
        //                            {
        //                                if (_objXMLNode.Attributes[_IterXMLNode].Name.CompareTo("Name") == 0)
        //                                {
        //                                    if (_SStateName.CompareTo(_objXMLNode.Attributes[0].Value) == 0)
        //                                    {
        //                                        bContinue = true;
        //                                        continue;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        if (false == bContinue)
        //                        {
        //                            XmlElement State = _XDoc.CreateElement("State");
        //                            XmlAttribute StateName = _XDoc.CreateAttribute("Name");
        //                            StateName.Value = _SStateName;
        //                            State.SetAttributeNode(StateName);
        //                            _objBaseNode.PrependChild(State);
        //                            _objBaseNode.InsertAfter(State, _objBaseNode.LastChild);
        //                            FileStream Xml = new FileStream(@"D:\FinalNewMicro\Hrms_Common\Country.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
        //                            _XDoc.Save(Xml);
        //                            _SStateName = _SStateName;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return _SStateName;
        //}



        //*******************************Collections************************************




        //********************************remaining*************************


        //public Collection<Country> fn_getCountry()
        //{
        //    Collection<Country> CountryList = new Collection<Country>();
        //    _Connection = Con.fn_MainConnection();
        //    SqlCommand _SCountry = new SqlCommand("select * from common_Country", _Connection);
        //    _Connection.Open();
        //    SqlDataReader dr = _SCountry.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        Country country = new Country();
        //        country.CountryName = Convert.IsDBNull(dr["v_CountryName"]) ? "" : (string)dr["v_CountryName"];
        //        CountryList.Add(country);
        //    }
        //    return CountryList;
        //}        

        //public Collection<State> fn_getState(int j)
        //{
        //    Collection<State> StateList = new Collection<State>();
        //    _Connection = Con.fn_MainConnection();
        //    SqlCommand _SState = new SqlCommand("select * from common_State where fn_CountryId='" + j + "'", _Connection);
        //    _Connection.Open();
        //    SqlDataReader dr_State = _SState.ExecuteReader();
        //    while (dr_State.Read())
        //    {
        //        State state = new State();
        //        state.StateName = Convert.IsDBNull(dr_State["v_StateName"]) ? "" : (string)dr_State["v_StateName"];
        //        StateList.Add(state);
        //    }
        //    return StateList;
        //}         




        public Collection<Company> fn_getCompanybycode(int Code)
        {
            Collection<Company> CompanyList = new Collection<Company>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select pn_CompanyID,CompanyCode,CompanyName,Address_Line1,Address_Line2,";
            _SqlString += "City,State,Country,ZipCode,Phone_No,Fax_No,Email_Id,AlternateEmail_Id,";
            _SqlString += "Company_User_Id,Company_Password from paym_Company where pn_CompanyID =" + Code;
            SqlCommand _SSCompany = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Company = _SSCompany.ExecuteReader();
            while (dr_Company.Read())
            {
                Company company = new Company();
                company.CompanyId = (int)dr_Company["pn_CompanyID"];
                company.CompanyCode = Convert.IsDBNull(dr_Company["CompanyCode"]) ? "" : (string)dr_Company["CompanyCode"];
                company.CompanyName = Convert.IsDBNull(dr_Company["CompanyName"]) ? "" : (string)dr_Company["CompanyName"];
                company.AddressLine1 = Convert.IsDBNull(dr_Company["Address_Line1"]) ? "" : (string)dr_Company["Address_Line1"];
                company.AddressLine2 = Convert.IsDBNull(dr_Company["Address_Line2"]) ? "" : (string)dr_Company["Address_Line2"];
                company.City = Convert.IsDBNull(dr_Company["City"]) ? "" : (string)dr_Company["City"];
                company.StateName = Convert.IsDBNull(dr_Company["State"]) ? "" : (string)dr_Company["State"];
                company.CountryName = Convert.IsDBNull(dr_Company["Country"]) ? "" : (string)dr_Company["Country"];
                company.ZipCode = Convert.IsDBNull(dr_Company["ZipCode"]) ? "" : (string)dr_Company["ZipCode"];
                company.PhoneNo = Convert.IsDBNull(dr_Company["Phone_No"]) ? "" : (string)dr_Company["Phone_No"];
                company.FaxNo = Convert.IsDBNull(dr_Company["Fax_No"]) ? "" : (string)dr_Company["Fax_No"];
                company.EmailId = Convert.IsDBNull(dr_Company["Email_Id"]) ? "" : (string)dr_Company["Email_Id"];
                company.Alternate_EmailId = Convert.IsDBNull(dr_Company["AlternateEmail_Id"]) ? "" : (string)dr_Company["AlternateEmail_Id"];
                company.UserId = Convert.IsDBNull(dr_Company["Company_User_Id"]) ? "" : (string)dr_Company["Company_User_Id"];
                company.Password = Convert.IsDBNull(dr_Company["Company_Password"]) ? "" : (string)dr_Company["Company_Password"];
                CompanyList.Add(company);
            }
            return CompanyList;
        }

        public Collection<Company> fn_getBranchCompanybyCode(int Code)
        {
            Collection<Company> BranchCompanyList = new Collection<Company>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select pn_CompanyID,pn_BranchID,CompanyCode,CompanyName,Address_Line1,Address_Line2,";
            _SqlString += "City,ZipCode,Country,State,Phone_No,Fax_No,Email_Id,AlternateEmail_Id,Company_User_Id,";
            _SqlString += "Company_Password from paym_Branch_company where pn_BranchID = " + Code;
            SqlCommand _SSCompany = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Company = _SSCompany.ExecuteReader();
            while (dr_Company.Read())
            {
                Company company = new Company();
                company.CompanyId = (int)dr_Company["pn_CompanyID"];
                company.BranchCompanyId = (int)dr_Company["pn_BranchID"];
                company.CompanyCode = Convert.IsDBNull(dr_Company["CompanyCode"]) ? "" : (string)dr_Company["CompanyCode"];
                company.CompanyName = Convert.IsDBNull(dr_Company["CompanyName"]) ? "" : (string)dr_Company["CompanyName"];
                company.AddressLine1 = Convert.IsDBNull(dr_Company["Address_Line1"]) ? "" : (string)dr_Company["Address_Line1"];
                company.AddressLine2 = Convert.IsDBNull(dr_Company["Address_Line2"]) ? "" : (string)dr_Company["Address_Line2"];
                company.City = Convert.IsDBNull(dr_Company["City"]) ? "" : (string)dr_Company["City"];
                company.StateName = Convert.IsDBNull(dr_Company["State"]) ? "" : (string)dr_Company["State"];
                company.CountryName = Convert.IsDBNull(dr_Company["Country"]) ? "" : (string)dr_Company["Country"];
                company.ZipCode = Convert.IsDBNull(dr_Company["ZipCode"]) ? "" : (string)dr_Company["ZipCode"];
                company.PhoneNo = Convert.IsDBNull(dr_Company["Phone_No"]) ? "" : (string)dr_Company["Phone_No"];
                company.FaxNo = Convert.IsDBNull(dr_Company["Fax_No"]) ? "" : (string)dr_Company["Fax_No"];
                company.EmailId = Convert.IsDBNull(dr_Company["Email_Id"]) ? "" : (string)dr_Company["Email_Id"];
                company.Alternate_EmailId = Convert.IsDBNull(dr_Company["AlternateEmail_Id"]) ? "" : (string)dr_Company["AlternateEmail_Id"];
                company.UserId = Convert.IsDBNull(dr_Company["Company_User_Id"]) ? "" : (string)dr_Company["Company_User_Id"];
                company.Password = Convert.IsDBNull(dr_Company["Company_Password"]) ? "" : (string)dr_Company["Company_Password"];
                BranchCompanyList.Add(company);
            }
            return BranchCompanyList;
        }

        public void CompanyDelete(int CompanyId, string DataBase)
        {
            _Connection = Con.fn_Connection();
            string _DelCommand = "Delete from common_Company where pn_CompanyId =" + CompanyId;
            SqlCommand cmd = new SqlCommand(_DelCommand, _Connection);
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
        }

        public void fn_CompanyCreation()
        {
            Con.fn_Connection();
        }

        public int fn_getCompanyId(string _pCompanyCode)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select pn_CompanyID from paym_Company where CompanyCode ='" + _pCompanyCode + "'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Company = _Cmd.ExecuteReader();
            Company company = new Company();
            while (dr_Company.Read())
            {
                company.CompanyId = (int)dr_Company["pn_CompanyID"];
            }
            return company.CompanyId;
        }



//*******************************NEW************************************


        public void Company_Create(Company c)
        {
            try
            {
            _Connection = Con.fn_Connection();
            SqlCommand _ISPCompany = new SqlCommand("sp_paym_Company", _Connection);
            _ISPCompany.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPCompanyParam = new SqlParameter[19];

            _ISPCompanyParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPCompanyParam[0].Value = c.CompanyId;
            _ISPCompanyParam[1] = new SqlParameter("@CompanyCode", SqlDbType.VarChar);
            _ISPCompanyParam[1].Value = c.CompanyCode;
            _ISPCompanyParam[2] = new SqlParameter("@CompanyName", SqlDbType.VarChar);
            _ISPCompanyParam[2].Value = c.CompanyName;


            _ISPCompanyParam[3] = new SqlParameter("@HeadOfficeCode", SqlDbType.VarChar);
            _ISPCompanyParam[3].Value = c.temp1;
            _ISPCompanyParam[4] = new SqlParameter("@HeadOfficeName", SqlDbType.VarChar);
            _ISPCompanyParam[4].Value = c.temp2;

            _ISPCompanyParam[5] = new SqlParameter("@Address_Line1", SqlDbType.VarChar);
            _ISPCompanyParam[5].Value = c.AddressLine1;
            _ISPCompanyParam[6] = new SqlParameter("@Address_Line2", SqlDbType.VarChar);
            _ISPCompanyParam[6].Value = c.AddressLine2;
            _ISPCompanyParam[7] = new SqlParameter("@City", SqlDbType.VarChar);
            _ISPCompanyParam[7].Value = c.City;
            _ISPCompanyParam[8] = new SqlParameter("@ZipCode", SqlDbType.VarChar);
            _ISPCompanyParam[8].Value = c.ZipCode;
            _ISPCompanyParam[9] = new SqlParameter("@Country", SqlDbType.VarChar);
            _ISPCompanyParam[9].Value = c.CountryName;
            _ISPCompanyParam[10] = new SqlParameter("@State", SqlDbType.VarChar);
            _ISPCompanyParam[10].Value = c.StateName;
            _ISPCompanyParam[11] = new SqlParameter("@Phone_No", SqlDbType.VarChar);
            _ISPCompanyParam[11].Value = c.PhoneNo;
            _ISPCompanyParam[12] = new SqlParameter("@Fax_No", SqlDbType.VarChar);
            _ISPCompanyParam[12].Value = c.FaxNo;
            _ISPCompanyParam[13] = new SqlParameter("@Email_Id", SqlDbType.VarChar);
            _ISPCompanyParam[13].Value = c.EmailId;
            _ISPCompanyParam[14] = new SqlParameter("@AlternateEmail_Id", SqlDbType.VarChar);
            _ISPCompanyParam[14].Value = c.Alternate_EmailId;

            _ISPCompanyParam[15] = new SqlParameter("@User_Id", SqlDbType.VarChar);
            _ISPCompanyParam[15].Value = c.UserId;
            _ISPCompanyParam[16] = new SqlParameter("@Password", SqlDbType.VarChar);
            _ISPCompanyParam[16].Value = c.Password;

            _ISPCompanyParam[17] = new SqlParameter("@Role", SqlDbType.Char);
            _ISPCompanyParam[17].Value = c.role;
            _ISPCompanyParam[18] = new SqlParameter("@Status", SqlDbType.Char);
            _ISPCompanyParam[18].Value = c.status;



            for (int i = 0; i < _ISPCompanyParam.Length; i++)
            {
                _ISPCompany.Parameters.Add(_ISPCompanyParam[i]);
            }


            _Connection.Open();
            _ISPCompany.ExecuteNonQuery();
            _Connection.Close();


            //return "0";
            }
            catch (Exception objEx)
            {
                
            }
        }


        public void Branch_Create(Company c)
        {

            //    try
            //    {

            _Connection = Con.fn_Connection();
            SqlCommand _ISPCompany = new SqlCommand("sp_paym_Branch", _Connection);
            _ISPCompany.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPCompanyParam = new SqlParameter[24];
            _ISPCompanyParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPCompanyParam[0].Value = c.CompanyId;
            _ISPCompanyParam[1] = new SqlParameter("@pn_BranchId", SqlDbType.Int);
            _ISPCompanyParam[1].Value = c.BranchCompanyId;
            _ISPCompanyParam[2] = new SqlParameter("@BranchCode", SqlDbType.VarChar);
            _ISPCompanyParam[2].Value = c.CompanyCode;
            _ISPCompanyParam[3] = new SqlParameter("@BranchName", SqlDbType.VarChar);
            _ISPCompanyParam[3].Value = c.CompanyName;
            _ISPCompanyParam[4] = new SqlParameter("@Address_Line1", SqlDbType.VarChar);
            _ISPCompanyParam[4].Value = c.AddressLine1;
            _ISPCompanyParam[5] = new SqlParameter("@Address_Line2", SqlDbType.VarChar);
            _ISPCompanyParam[5].Value = c.AddressLine2;
            _ISPCompanyParam[6] = new SqlParameter("@City", SqlDbType.VarChar);
            _ISPCompanyParam[6].Value = c.City;
            _ISPCompanyParam[7] = new SqlParameter("@ZipCode", SqlDbType.VarChar);
            _ISPCompanyParam[7].Value = c.ZipCode;
            _ISPCompanyParam[8] = new SqlParameter("@Country", SqlDbType.VarChar);
            _ISPCompanyParam[8].Value = c.CountryName;
            _ISPCompanyParam[9] = new SqlParameter("@State", SqlDbType.VarChar);
            _ISPCompanyParam[9].Value = c.StateName;
            _ISPCompanyParam[10] = new SqlParameter("@Phone_No", SqlDbType.VarChar);
            _ISPCompanyParam[10].Value = c.PhoneNo;
            _ISPCompanyParam[11] = new SqlParameter("@Fax_No", SqlDbType.VarChar);
            _ISPCompanyParam[11].Value = c.FaxNo;
            _ISPCompanyParam[12] = new SqlParameter("@Email_Id", SqlDbType.VarChar);
            _ISPCompanyParam[12].Value = c.EmailId;
            _ISPCompanyParam[13] = new SqlParameter("@AlternateEmail_Id", SqlDbType.VarChar);
            _ISPCompanyParam[13].Value = c.Alternate_EmailId;
            _ISPCompanyParam[14] = new SqlParameter("@Branch_User_Id", SqlDbType.VarChar);
            _ISPCompanyParam[14].Value = c.UserId;
            _ISPCompanyParam[15] = new SqlParameter("@Branch_Password", SqlDbType.VarChar);
            _ISPCompanyParam[15].Value = c.Password;
            _ISPCompanyParam[16] = new SqlParameter("@Branch_out", SqlDbType.Int);
            _ISPCompanyParam[16].Value = ParameterDirection.Output;
            _ISPCompanyParam[17] = new SqlParameter("@Role", SqlDbType.Char);
            _ISPCompanyParam[17].Value = c.role;
            _ISPCompanyParam[18] = new SqlParameter("@Status", SqlDbType.Char);
            _ISPCompanyParam[18].Value = c.status;
            _ISPCompanyParam[19] = new SqlParameter("@PFno", SqlDbType.VarChar);
            _ISPCompanyParam[19].Value = c.PFno;
            _ISPCompanyParam[20] = new SqlParameter("@ESIno", SqlDbType.VarChar);
            _ISPCompanyParam[20].Value = c.ESIno;
            _ISPCompanyParam[21] = new SqlParameter("@StartDate", SqlDbType.DateTime);
            _ISPCompanyParam[21].Value = c.Start_date;
            _ISPCompanyParam[22] = new SqlParameter("@EndDate", SqlDbType.DateTime);
            _ISPCompanyParam[22].Value = c.End_date;
            _ISPCompanyParam[23] = new SqlParameter("@branch_status", SqlDbType.Char);
            _ISPCompanyParam[23].Value = c.status;


            for (int i = 0; i < _ISPCompanyParam.Length; i++)
            {
                _ISPCompany.Parameters.Add(_ISPCompanyParam[i]);
            }
            _Connection.Open();
            _ISPCompany.ExecuteNonQuery();
            _Connection.Close();


            //return "0";
            //}
            //catch (Exception objEx)
            //{
            //    return "1";
            //}


        }

        public Collection<Company> fn_getCompany()
        {
            Collection<Company> CompanyList=new Collection<Company>() ;
            _Connection = Con.fn_Connection();          

            string _SqlString = "select * from paym_Company";
            SqlCommand _SSCompany = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Company = _SSCompany.ExecuteReader();
            while (dr_Company.Read())
            {
                Company company = new Company();
                company.CompanyId = (int)dr_Company["pn_CompanyID"];
                company.CompanyCode = Convert.IsDBNull(dr_Company["CompanyCode"]) ? "" : (string)dr_Company["CompanyCode"];
                company.CompanyName = Convert.IsDBNull(dr_Company["CompanyName"]) ? "" : (string)dr_Company["CompanyName"];
                company.AddressLine1 = Convert.IsDBNull(dr_Company["Address_Line1"]) ? "" : (string)dr_Company["Address_Line1"];
                company.AddressLine2 = Convert.IsDBNull(dr_Company["Address_Line2"]) ? "" : (string)dr_Company["Address_Line2"];
                company.City = Convert.IsDBNull(dr_Company["City"]) ? "" : (string)dr_Company["City"];
                company.ZipCode = Convert.IsDBNull(dr_Company["ZipCode"]) ? "" : (string)dr_Company["ZipCode"];
                company.CountryName = Convert.IsDBNull(dr_Company["Country"]) ? "" : (string)dr_Company["Country"];
                company.StateName = Convert.IsDBNull(dr_Company["State"]) ? "" : (string)dr_Company["State"];
                company.PhoneNo = Convert.IsDBNull(dr_Company["Phone_No"]) ? "" : (string)dr_Company["Phone_No"];
                company.FaxNo = Convert.IsDBNull(dr_Company["Fax_No"]) ? "" : (string)dr_Company["Fax_No"];
                company.EmailId = Convert.IsDBNull(dr_Company["Email_Id"]) ? "" : (string)dr_Company["Email_Id"];
                company.Alternate_EmailId = Convert.IsDBNull(dr_Company["AlternateEmail_Id"]) ? "" : (string)dr_Company["AlternateEmail_Id"];
                company.PFCode = Convert.IsDBNull(dr_Company["PFno"]) ? "" : (string)dr_Company["PFno"];
                company.ESICode = Convert.IsDBNull(dr_Company["Esino"]) ? "" : (string)dr_Company["Esino"];
                company.Start_date = (DateTime)dr_Company["Start_Date"];
                company.End_date = (DateTime)dr_Company["End_Date"];
                CompanyList.Add(company);
            }
            return CompanyList;
        }

        public Collection<Company> fn_getBranchCompany(int b_id)
        {
            Collection<Company> BranchCompanyList = new Collection<Company>();
            _Connection = Con.fn_Connection();

            string _SqlString = "select * from paym_Branch where pn_BranchID=" + b_id + "";
            SqlCommand _SSBranchCompany = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_BranchCompany = _SSBranchCompany.ExecuteReader();
            while (dr_BranchCompany.Read())
            {
                Company BranchCompany = new Company();

                BranchCompany.CompanyId = (int)dr_BranchCompany["pn_BranchID"];
                BranchCompany.CompanyCode = Convert.IsDBNull(dr_BranchCompany["BranchCode"]) ? "" : (string)dr_BranchCompany["BranchCode"];
                BranchCompany.CompanyName = Convert.IsDBNull(dr_BranchCompany["BranchName"]) ? "" : (string)dr_BranchCompany["BranchName"];
                BranchCompany.AddressLine1 = Convert.IsDBNull(dr_BranchCompany["Address_Line1"]) ? "" : (string)dr_BranchCompany["Address_Line1"];
                BranchCompany.AddressLine2 = Convert.IsDBNull(dr_BranchCompany["Address_Line2"]) ? "" : (string)dr_BranchCompany["Address_Line2"];
                BranchCompany.City = Convert.IsDBNull(dr_BranchCompany["City"]) ? "" : (string)dr_BranchCompany["City"];
                BranchCompany.ZipCode = Convert.IsDBNull(dr_BranchCompany["ZipCode"]) ? "" : (string)dr_BranchCompany["ZipCode"];
                BranchCompany.CountryName = Convert.IsDBNull(dr_BranchCompany["Country"]) ? "" : (string)dr_BranchCompany["Country"];
                BranchCompany.StateName = Convert.IsDBNull(dr_BranchCompany["State"]) ? "" : (string)dr_BranchCompany["State"];
                BranchCompany.PhoneNo = Convert.IsDBNull(dr_BranchCompany["Phone_No"]) ? "" : (string)dr_BranchCompany["Phone_No"];
                BranchCompany.FaxNo = Convert.IsDBNull(dr_BranchCompany["Fax_No"]) ? "" : (string)dr_BranchCompany["Fax_No"];
                BranchCompany.EmailId = Convert.IsDBNull(dr_BranchCompany["Email_Id"]) ? "" : (string)dr_BranchCompany["Email_Id"];
                BranchCompany.Alternate_EmailId = Convert.IsDBNull(dr_BranchCompany["AlternateEmail_Id"]) ? "" : (string)dr_BranchCompany["AlternateEmail_Id"];
                BranchCompany.UserId = Convert.IsDBNull(dr_BranchCompany["Branch_User_Id"]) ? "" : (string)dr_BranchCompany["Branch_User_Id"];
                BranchCompany.Password = Convert.IsDBNull(dr_BranchCompany["Branch_Password"]) ? "" : (string)dr_BranchCompany["Branch_Password"];
                BranchCompany.PFCode = Convert.IsDBNull(dr_BranchCompany["PFno"]) ? "" : (string)dr_BranchCompany["PFno"];
                BranchCompany.ESICode = Convert.IsDBNull(dr_BranchCompany["ESIno"]) ? "" : (string)dr_BranchCompany["ESIno"];
                BranchCompany.Start_date = (DateTime)dr_BranchCompany["Start_Date"];
                BranchCompany.End_date = (DateTime)dr_BranchCompany["End_Date"];

                BranchCompanyList.Add(BranchCompany);
            }
            return BranchCompanyList;
        }


        public Collection<Company> fn_getCompanycode(string c_Code)
        {
            Collection<Company> CompanyList = new Collection<Company>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Company where CompanyCode='" + c_Code + "'";
            SqlCommand _SSCompany = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Company = _SSCompany.ExecuteReader();
            while (dr_Company.Read())
            {
                Company company = new Company();

                company.CompanyCode = Convert.IsDBNull(dr_Company["CompanyCode"]) ? "" : (string)dr_Company["CompanyCode"];
                company.CompanyId = (int)dr_Company["pn_CompanyID"];

                CompanyList.Add(company);
            }
            return CompanyList;
        }

        public Collection<Company> fn_getBranchcode(string b_Code)
        {
            Collection<Company> CompanyList = new Collection<Company>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Branch where BranchCode='" + b_Code + "'";
            SqlCommand _SSCompany = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Company = _SSCompany.ExecuteReader();
            while (dr_Company.Read())
            {
                Company company = new Company();

                company.CompanyCode = Convert.IsDBNull(dr_Company["BranchCode"]) ? "" : (string)dr_Company["BranchCode"];
                company.BranchCompanyId = (int)dr_Company["pn_BranchID"];
                CompanyList.Add(company);
            }
            return CompanyList;
        }

        public Collection<Company> fn_getBranchs()
        {
            Collection<Company> CompanyList = new Collection<Company>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Branch where status='Y'";
            SqlCommand _SSCompany = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Company = _SSCompany.ExecuteReader();
            while (dr_Company.Read())
            {
                Company company = new Company();
                company.CompanyId = (int)dr_Company["pn_BranchID"];
                company.CompanyCode = Convert.IsDBNull(dr_Company["BranchCode"]) ? "" : (string)dr_Company["BranchCode"];
                company.CompanyName = Convert.IsDBNull(dr_Company["BranchName"]) ? "" : (string)dr_Company["BranchName"];
                CompanyList.Add(company);
            }
            return CompanyList;
        }

        public Collection<Company> fn_getPassword(string uid)
        {
            Collection<Company> PasswordList = new Collection<Company>();
            _Connection = Con.fn_Connection();
            //string _SqlString = "select * from paym_Employee where EmployeeCode='" + uid + "' and Password='" + pwd + "'";
            string _SqlString = "select * from paym_Employee where EmployeeCode='" + uid + "'";
            SqlCommand _SSCompany = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Company = _SSCompany.ExecuteReader();
            while (dr_Company.Read())
            {
                Company company = new Company();

                company.Password = Convert.IsDBNull(dr_Company["Password"]) ? "" : (string)dr_Company["Password"];

                PasswordList.Add(company);
            }
            return PasswordList;
        }

        public void  fn_get_Update_Pwd(char s, string u_id, string opwd, string npwd)
        {

            if (s == 'a')
            {

                _Connection = Con.fn_Connection();

                _Connection.Open();

                SqlCommand _Temppwd = new SqlCommand("Update paym_Employee set Password='" + npwd + "' where EmployeeCode='" + u_id + "' and Password='" + opwd + "'", _Connection);

                _Temppwd.ExecuteNonQuery();

                _Connection.Close();

            }
            else if (s == 'h' || s == 'd')
            {

                _Connection = Con.fn_Connection();

                _Connection.Open();

                SqlCommand _Temppwd = new SqlCommand("Update paym_Employee set Password='" + npwd + "' where EmployeeCode='" + u_id + "' and Password='" + opwd + "'", _Connection);

                _Temppwd.ExecuteNonQuery();

                SqlCommand _Branchpwd = new SqlCommand("Update paym_Branch set Branch_Password='" + npwd + "' where Branch_User_Id='" + u_id + "' and Branch_Password='" + opwd + "'", _Connection);

                _Branchpwd.ExecuteNonQuery();

                _Connection.Close();

            }
            else if (s == 'e')
            {

                _Connection = Con.fn_Connection();

                _Connection.Open();
                
                SqlCommand _Emppwd = new SqlCommand("Update paym_Employee set Password='" + npwd + "' where EmployeeCode='" + u_id + "' and Password='" + opwd + "'", _Connection);
                
                _Emppwd.ExecuteNonQuery();
                
                _Connection.Close();

            }

        }

        public void fn_sms_Update_Pwd(char s, string u_id, string opwd, string npwd)
        {

            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _Temppwd = new SqlCommand("Update sms_settings set Password='" + npwd + "' where  Password='" + opwd + "'", _Connection);

            _Temppwd.ExecuteNonQuery();

            _Connection.Close();

        }

        public DataSet fn_get_Login_Temp(string l_uid, string l_pwd)
        {
            _Connection = Con.fn_Connection();
            
            string _sample = "select * from paym_Employee where EmployeeCode='" + l_uid + "' and Password='" + l_pwd + "' and status='Y'";
            
            _Connection.Open();

            SqlDataAdapter _Ad_sample = new SqlDataAdapter(_sample, _Connection);

            DataSet _Ds_sample = new DataSet();

            _Ad_sample.Fill(_Ds_sample, "paym_Employee");

            _Connection.Close();
            
            return _Ds_sample;
        }

        public DataSet fn_get_Login_Employee(string l_uid, string l_pwd)
        {
            
            _Connection = Con.fn_Connection();
            
            string _sample = "select  pn_DesingnationId,* From paym_Employee e join paym_Employee_profile1 p on e.pn_EmployeeID= p.pn_EmployeeID where EmployeeCode ='" + l_uid + "' and Password ='" + l_pwd + "' and status='Y'";
            
            _Connection.Open();

            SqlDataAdapter _Ad_sample = new SqlDataAdapter(_sample, _Connection);

            DataSet _Ds_sample = new DataSet();

            _Ad_sample.Fill(_Ds_sample, "paym_Employee");

            _Connection.Close();

            return _Ds_sample;
            
        }
        public DataSet fn_get_Login_Userrights(int eid)
        {
            
            _Connection = Con.fn_Connection();

            string _sample = "select * from user_rights where pn_EmployeeID =" + eid + "";
            
            _Connection.Open();

            SqlDataAdapter _Ad_sample = new SqlDataAdapter(_sample, _Connection);

            DataSet _Ds_sample = new DataSet();

            _Ad_sample.Fill(_Ds_sample, "user_rights");

            _Connection.Close();

            return _Ds_sample;
            
        }

        public DataSet check_Userrights(int eid,string str_form)
        {
            _Connection = Con.fn_Connection();
            //string _sample = "select * from user_rights where pn_EmployeeID =" + eid + " and "+str_form+"='Y'";

            string _sample = "select * from user_Authentications where pn_EmployeeID =" + eid + " and pn_FormID=" + str_form + " and status='Y'";

            _Connection.Open();

            SqlDataAdapter _Ad_sample = new SqlDataAdapter(_sample, _Connection);

            DataSet _Ds_sample = new DataSet();

            _Ad_sample.Fill(_Ds_sample, "user_rights");

            _Connection.Close();

            return _Ds_sample;
        }

        public Collection<Company> fn_get_Password(string uid, string pwd)
        {
            Collection<Company> PasswordList = new Collection<Company>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Employee where EmployeeCode='" + uid + "' and Password='" + pwd + "'";
            SqlCommand _SSCompany = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Company = _SSCompany.ExecuteReader();
            while (dr_Company.Read())
            {
                Company company = new Company();
                PasswordList.Add(company);
            }
            return PasswordList;
        }
        public Collection<Company> fn_get_Availablity(string uid)
        {
            Collection<Company> PasswordList = new Collection<Company>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Employee where EmployeeCode='" + uid + "'";
            SqlCommand _SSCompany = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Company = _SSCompany.ExecuteReader();
            while (dr_Company.Read())
            {
                Company company = new Company();              

                PasswordList.Add(company);
            }
            return PasswordList;
        }

        public string fn_Test()
        {
              Collection<Company> CompanyList = new Collection<Company>();
                _Connection = Con.fn_Connection();
                string _SqlString = "select * paym_Branch";
                SqlCommand _SSCompany = new SqlCommand(_SqlString, _Connection);
                _Connection.Open();
                SqlDataReader dr_Company = _SSCompany.ExecuteReader();
                while (dr_Company.Read())
                {
                    Company company = new Company();

                    company.CompanyId = (int)dr_Company["pn_BranchID"];
                    company.CompanyCode = Convert.IsDBNull(dr_Company["BranchCode"]) ? "" : (string)dr_Company["BranchCode"];
                    company.CompanyName = Convert.IsDBNull(dr_Company["BranchName"]) ? "" : (string)dr_Company["BranchName"];

                    CompanyList.Add(company);
                }

                return "f Not Error";
            }




//Deletion Process


        public void Delete_All(string str_del)
        {
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand(str_del, _Connection);
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
        }



        public DataSet Delete_Count(string delcount)
        {

            _Connection = Con.fn_Connection();
         
            _Connection.Open();

            SqlDataAdapter _Ad_sample = new SqlDataAdapter(delcount, _Connection);

            DataSet _Ds_sample = new DataSet();

            _Ad_sample.Fill(_Ds_sample);

            _Connection.Close();

            return _Ds_sample;

        }

        public DataSet check_UserAuthentication(int eid, string str_form)
        {

            _Connection = Con.fn_Connection();

            string _sample = "select * from user_Authentications where pn_EmployeeID =" + eid + " and pn_FormID=" + str_form + " and status='Y'";

            _Connection.Open();
            
            SqlDataAdapter Da_User = new SqlDataAdapter(_sample, _Connection);

            DataSet Ds_User = new DataSet();

            Da_User.Fill(Ds_User, "user_rights");

            _Connection.Close();

            return Ds_User;

        }

        public DataSet fn_get_Login_UserAuthentication(int eid)
        {
            string now = DateTime.Now.ToString("dd/MM/yyyy");
            string[] dt = now.Split('/','-');
            now = dt[2] + "/" + dt[1] + "/" + dt[0];
            _Connection = Con.fn_Connection();

            string _sample = "select * from user_Authentications where pn_EmployeeID =" + eid + " and status='y' and from_date <= '"+now+"' and to_date >= '"+now+"'";

            _Connection.Open();

            SqlDataAdapter Da_User = new SqlDataAdapter(_sample, _Connection);

            DataSet Ds_user = new DataSet();

            Da_User.Fill(Ds_user, "user_rights");

            _Connection.Close();

            return Ds_user;

        }


    }


}







