using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace ePayHrms.Login
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    public class EmployeeLogin
    {
        public EmployeeLogin()
        {
        }
        private string _EmployeeId;
        private string _EmployeePassword;
        private string _EmployeeName;
        private string _CompanyCode;

        public string EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }

        public string EmployeePassword
        {
            get { return _EmployeePassword; }
            set { _EmployeePassword = value; }
        }

        public string EmployeeName
        {
            get { return _EmployeeName; }
            set { _EmployeeName = value; }
        }

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set { _CompanyCode = value; }
        }

        public ArrayList fn_getNavigateUrl(string UserName, string Password)
        {
            ArrayList LoginList = new ArrayList();
            int _RootIter, _ChildNodeRoot, _SiblingChildNode;
            XmlDocument _doc = new XmlDocument();
            _doc.Load(@"D:\FinalNewMicro\Hrms_Common\Company.xml");
            XmlNodeList _objNodeList = _doc.GetElementsByTagName("Root");
            if (null == _objNodeList)
                return null;
            for (_RootIter = 0; _RootIter < _objNodeList.Count; _RootIter++)
            {
                XmlNode _objNode = _objNodeList.Item(_RootIter);
                if (null == _objNode)
                    continue;
                if (_objNode.Attributes[1].Name.CompareTo("UserId") == 0)
                {
                    if (_objNode.Attributes[2].Name.CompareTo("Password") == 0)
                    {
                        if ((UserName.CompareTo(_objNode.Attributes[1].Value) == 0) && (Password.CompareTo(_objNode.Attributes[2].Value) == 0))
                        {
                            LoginList.Add(_objNode.Attributes[0].Value);
                            LoginList.Add(_objNode.Attributes[1].Value);
                            LoginList.Add(_objNode.Attributes[2].Value);
                            LoginList.Add(_objNode.Attributes[3].Value);
                            LoginList.Add(_objNode.Attributes[4].Value);
                        }
                        else
                        {
                            XmlNodeList _ObjNodeChildList = _objNode.ChildNodes;
                            if (null == _ObjNodeChildList)
                                continue;
                            for (_ChildNodeRoot = 0; _ChildNodeRoot < _ObjNodeChildList.Count; _ChildNodeRoot++)
                            {
                                XmlNode _ObjChildNode = _ObjNodeChildList.Item(_ChildNodeRoot);
                                if (null == _ObjChildNode)
                                    continue;
                                if (_ObjChildNode.Attributes[1].Name.CompareTo("UserId") == 0)
                                {
                                    if (_ObjChildNode.Attributes[2].Name.CompareTo("Password") == 0)
                                    {
                                        if ((UserName.CompareTo(_ObjChildNode.Attributes[1].Value) == 0) && (Password.CompareTo(_ObjChildNode.Attributes[2].Value) == 0))
                                        {
                                            LoginList.Add(_ObjChildNode.Attributes[0].Value);
                                            LoginList.Add(_ObjChildNode.Attributes[1].Value);
                                            LoginList.Add(_ObjChildNode.Attributes[2].Value);
                                            LoginList.Add(_ObjChildNode.Attributes[3].Value);
                                            LoginList.Add(_ObjChildNode.Attributes[4].Value);
                                        }
                                        else
                                        {
                                            XmlNodeList _ObjChildNodeList = _ObjChildNode.ChildNodes;
                                            if (null == _ObjChildNodeList)
                                                continue;
                                            for (_SiblingChildNode = 0; _SiblingChildNode < _ObjChildNodeList.Count; _SiblingChildNode++)
                                            {
                                                XmlNode _ObjSiblingNode = _ObjChildNodeList.Item(_SiblingChildNode);
                                                if (null == _ObjSiblingNode)
                                                    continue;
                                                if (_ObjSiblingNode.Attributes[1].Name.CompareTo("UserId") == 0)
                                                {
                                                    if (_ObjSiblingNode.Attributes[2].Name.CompareTo("Password") == 0)
                                                    {
                                                        if ((UserName.CompareTo(_ObjSiblingNode.Attributes[1].Value) == 0) && (Password.CompareTo(_ObjSiblingNode.Attributes[2].Value) == 0))
                                                        {
                                                            LoginList.Add(_ObjSiblingNode.Attributes[0].Value);
                                                            LoginList.Add(_ObjSiblingNode.Attributes[1].Value);
                                                            LoginList.Add(_ObjSiblingNode.Attributes[2].Value);
                                                            LoginList.Add(_ObjSiblingNode.Attributes[3].Value);
                                                            LoginList.Add(_ObjSiblingNode.Attributes[4].Value);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return LoginList;
        }
    }
}
