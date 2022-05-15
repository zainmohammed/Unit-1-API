using BLL;
using BOL;
using EzDeliveryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EzDeliveryAPI.Controllers
{
    public class LookupController : ApiController
    {
        LookupBs lookupBs;
        public LookupController()
        {
            lookupBs = new LookupBs();
        }


        [HttpGet]
        public ResponseObj<List<Category>> GetAllCategory()
        {
            ResponseObj<List<Category>> returnObj = new ResponseObj<List<Category>>();
            List<Category> listcategory = new List<Category>();
            try
            {
                listcategory = lookupBs.GetAllCategory();
                if (listcategory.Count > 0)
                {
                    returnObj.Data = listcategory.OrderByDescending(x => x.CategoryId).ToList();
                    returnObj.isSuccess = true;
                    returnObj.Message = "List Of Category";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "List Of Category Not Found";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Lookup,  GetAllCategory");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        [HttpGet]
        public ResponseObj<List<Lookup_Country>> GetAllCountry()
        {
            ResponseObj<List<Lookup_Country>> returnObj = new ResponseObj<List<Lookup_Country>>();
            List<Lookup_Country> listcountry = new List<Lookup_Country>();
            listcountry = lookupBs.GetAllCountry();
            try
            {
                if (listcountry.Count > 0)
                {
                    returnObj.Data = listcountry;
                    returnObj.isSuccess = true;
                    returnObj.Message = "List of Countries";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "List of Countries Not Found";
                }

            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Lookup,  GetAllCountry");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        [HttpGet]
        public ResponseObj<List<Lookup_City>> GetCitiesByCountry(int countryId)
        {
            ResponseObj<List<Lookup_City>> returnObj = new ResponseObj<List<Lookup_City>>();
            List<Lookup_City> listcity = new List<Lookup_City>();
            listcity = lookupBs.GetCitiesByCountry(countryId);
            try
            {
                if (listcity.Count > 0)
                {
                    returnObj.Data = listcity;
                    returnObj.isSuccess = true;
                    returnObj.Message = "List of Cities By Country";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "List of Cities By Country Not Found";
                }

            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Lookup,  GetCitiesByCountry");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        [HttpGet]
        public  ResponseObj<List<Lookup_OrderStatus>> GetAllJobStatus()
        {
            ResponseObj<List<Lookup_OrderStatus>> returnObj = new ResponseObj<List<Lookup_OrderStatus>>();
            List<Lookup_OrderStatus> listjobstatus = new List<Lookup_OrderStatus>();
            try
            {
                listjobstatus = lookupBs.GetAllJobStatus();
                if (listjobstatus.Count > 0)
                {
                    returnObj.Data = listjobstatus;
                    returnObj.isSuccess = true;
                    returnObj.Message = "List of JobStatus";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "List of JobStatus Not Found";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Lookup,  GetAllJobStatus");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }


    }
}
