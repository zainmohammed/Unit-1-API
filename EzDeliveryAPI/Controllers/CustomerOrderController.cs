using BLL;
using BOL;
using DAL;
using EzDeliveryAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using static DAL.ModelDb;

namespace EzDeliveryAPI.Controllers
{
    public class CustomerOrderController : ApiController
    {
        CustomerOrderBs customerorderBs;
        CustomerAddressBs customerAddressBs;
        OrderStatusLogBs orderStatusLogBs;
        CustomerBs customerBs;
        public CustomerOrderController()
        {
            customerorderBs = new CustomerOrderBs();
            customerAddressBs = new CustomerAddressBs();
            orderStatusLogBs = new OrderStatusLogBs();
            customerBs = new CustomerBs();
        }
        public ResponseObj<CustomerOrder> CreateOrder(CustomerOrderModel obj)
        {
            ResponseObj<CustomerOrder> returnObj = new ResponseObj<CustomerOrder>();
            CustomerOrder custorder = new CustomerOrder();
            CustomerAddress Pickup = new CustomerAddress();
            CustomerAddress Destination = new CustomerAddress();
            OrderStatusLog orderstatusLog = new OrderStatusLog();
            try
            {
                Pickup.Address = obj.PickupPoint.Address;

                Pickup.AddressTypeId = obj.PickupPoint.AddressTypeId;
                Pickup.AreaName = obj.PickupPoint.AreaName;
                Pickup.CustomerId = obj.PickupPoint.CustomerId;
                Pickup.Lat = obj.PickupPoint.Lat;
                Pickup.Long = obj.PickupPoint.Long;
                Pickup.StreetName = obj.PickupPoint.StreetName;
                Pickup.Zipcode = obj.PickupPoint.Zipcode;
                if (customerAddressBs.InsertCustomerAddress(Pickup))
                {
                    returnObj.isSuccess = true;
                    returnObj.Message = "Pickup Address Inserted Successfully";

                    Destination.Address = obj.DeliveryPoint.Address;
                    Destination.AddressTypeId = obj.DeliveryPoint.AddressTypeId;
                    Destination.AreaName = obj.DeliveryPoint.AreaName;
                    Destination.CustomerId = Pickup.CustomerId;
                    Destination.Lat = obj.DeliveryPoint.Lat;
                    Destination.Long = obj.DeliveryPoint.Long;
                    Destination.StreetName = obj.DeliveryPoint.StreetName;
                    Destination.Zipcode = obj.DeliveryPoint.Zipcode;
                    if (customerAddressBs.InsertCustomerAddress(Destination))
                    {
                        returnObj.isSuccess = true;
                        returnObj.Message = "Drop Address Inserted Successfully";

                        custorder.CustomerId = Destination.CustomerId;
                        custorder.OrderDetailText = obj.CustomerOrder.OrderDetailText;
                        custorder.OrderStatusId = 1;
                        custorder.CategoryId = obj.CustomerOrder.CategoryId;
                        custorder.PickupPoint = Pickup.CustomerAddressId;
                        custorder.DeliveryPoint = Destination.CustomerAddressId;
                        custorder.OrderDetailText = obj.CustomerOrder.OrderDetailText;
                        custorder.ValueOfGood = obj.CustomerOrder.ValueOfGood;
                        custorder.FromNumber = obj.CustomerOrder.FromNumber;
                        custorder.ToNumber = obj.CustomerOrder.ToNumber;
                        custorder.Weight = obj.CustomerOrder.Weight;
                        custorder.Size = obj.CustomerOrder.Size;
                        custorder.IsOrderFlag = false;

                        string httpPath = HttpContext.Current.Request.Url.AbsoluteUri.Replace("api", "|").Split('|')[0];
                        Common common = new Common();
                        if (obj.CustomerOrder.OrderDetailVoice != null)
                        {
                            var s = common.SaveImage(obj.CustomerOrder.OrderDetailVoice, "Customer", Convert.ToString(custorder.CustomerOrderId), "Documents");
                            custorder.OrderDetailVoice = httpPath + s;
                        }
                        if (obj.CustomerOrder.ProductPic != null)
                        {
                            var p = common.SaveImage(obj.CustomerOrder.ProductPic, "Employee", Convert.ToString(custorder.CustomerOrderId), "Image");
                            custorder.ProductPic = httpPath + p;
                        }

                        custorder.CustomerOrderNo = "CUS" + "-" + custorder.CustomerId + "-" + DateTime.Now.ToString("MMddmmssff");
                        custorder.Date = DateTime.Now;
                        if (customerorderBs.InsertCustomerOrder(custorder))
                        {

                            if (custorder.CustomerOrderId != 0)
                            {
                                Pickup.CustomerOrderId = custorder.CustomerOrderId;
                                customerAddressBs.UpdateCustomerAddress(Pickup);

                                if (custorder.CustomerOrderId != 0)
                                {
                                    Destination.CustomerOrderId = custorder.CustomerOrderId;
                                    customerAddressBs.UpdateCustomerAddress(Destination);

                                    orderstatusLog.OrderStatusId = custorder.OrderStatusId;
                                    orderstatusLog.CustomerOrderId = custorder.CustomerOrderId;
                                    orderstatusLog.OrderStatusDateTime = DateTime.Now;

                                    if (orderStatusLogBs.InsertOrderStatusLog(orderstatusLog))
                                    {
                                        returnObj.isSuccess = true;
                                        returnObj.Message = "Order Created Successfully";
                                        returnObj.Data = custorder;
                                    }
                                    else
                                    {
                                        returnObj.isSuccess = false;
                                        returnObj.Message = "Order Not Created";
                                    }

                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "CustomerOrder,  InsertOrder");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        public async Task<bool> Upload()
        {
            try
            {
                var fileuploadPath = ConfigurationManager.AppSettings["FileUploadLocation"];
                var provider = new MultipartFormDataStreamProvider(fileuploadPath);
                var content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                foreach (var header in Request.Content.Headers)
                {
                    content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                await content.ReadAsMultipartAsync(provider);

                //Code for renaming the random file to Original file name
                string uploadingFileName = provider.FileData.Select(x => x.LocalFileName).FirstOrDefault();
                string originalFileName = String.Concat(fileuploadPath, "\\" + (provider.Contents[0].Headers.ContentDisposition.FileName).Trim(new Char[] { '"' }));
                CustomerOrder custorder = new CustomerOrder();
                var CustomerOrder = customerorderBs.GetCustomerOrderById(custorder.CustomerOrderId);
                custorder.OrderDetailVoice = originalFileName;
                customerorderBs.InsertCustomerOrder(custorder);
                if (File.Exists(originalFileName))
                {
                    File.Delete(originalFileName);
                }

                File.Move(uploadingFileName, originalFileName);
                //Code renaming ends...

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        [HttpGet]
        public ResponseObj<List<CustomerOrder>> GetListofCustomerOrder(int vendorId)
        {
            ResponseObj<List<CustomerOrder>> returnObj = new ResponseObj<List<CustomerOrder>>();
            List<CustomerOrder> listcustomerorder = new List<CustomerOrder>();
            try
            {
                listcustomerorder = customerorderBs.GetListofCustomerOrder(vendorId);
                if (listcustomerorder.Count > 0)
                {

                    returnObj.isSuccess = true;
                    returnObj.Message = "List of Customer Order";
                    returnObj.Data = listcustomerorder;
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "No Customer Order Found";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "CustomerOrder,  GetListofCustomerOrder");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }
        [HttpPost]
        public ResponseObj<OrderDriver> DriverBidsOrder(OrderDriverModel obj)
        {
            ResponseObj<OrderDriver> returnObj = new ResponseObj<OrderDriver>();
            OrderDriver orderDriver = new OrderDriver();

            orderDriver.BidPrice = obj.BidPrice;
            orderDriver.CustomerOrderId = obj.CustomerOrderId;
            orderDriver.DriverId = obj.DriverId;
            orderDriver.IsSelected = obj.IsSelected;
            orderDriver.Time = obj.Time;
            orderDriver.DeliveryNotes = obj.DeliveryNotes;
            orderDriver.Status = 2;
            var cusOrder = customerorderBs.GetCustomerOrderById(obj.CustomerOrderId);
            if (cusOrder.OrderStatusId <= 2)
            {
                if (customerorderBs.InsertOrderDriver(orderDriver))
                {
                    cusOrder.OrderStatusId = 2;
                    cusOrder.OrderDriverId = obj.DriverId;
                    if (customerorderBs.UpdateCustomerOrder(cusOrder))
                    {
                        returnObj.Data = orderDriver;
                        returnObj.isSuccess = true;
                        returnObj.Message = "Order Driver Inserted Succesfully";
                    }
                    else
                    {
                        returnObj.isSuccess = false;
                        returnObj.Message = "Order Driver Insertion UnSuccesfully";
                    }
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Order Driver Insertion UnSuccesfully";
                }
            }
            else
            {
                returnObj.isSuccess = false;
                returnObj.Message = "Bid cannot be accepted";
            }

            return returnObj;
        }
        [HttpPost]
        public ResponseObj<CustomerOrder> CustomerAcceptsBid(int CustomerOrderId, int OrderDriverId)
        {
            ResponseObj<CustomerOrder> returnObj = new ResponseObj<CustomerOrder>();
            CustomerOrder custOrder = new CustomerOrder();
            custOrder = customerorderBs.GetCustomerOrderById(CustomerOrderId);
            if (custOrder != null)
            {
                var orderDriver = customerorderBs.getOrderDriverByOrderDriver(OrderDriverId);
                if (orderDriver != null)
                {
                    //custOrder.OrderDriverId = orderDriver.OrderDriverId;
                    custOrder.OrderStatusId = 3;
                    orderDriver.Status = 3;
                    if (customerorderBs.UpdateCustomerOrder(custOrder))
                    {
                        returnObj.Data = custOrder;
                        returnObj.isSuccess = true;
                        returnObj.Message = "Customer Accepts bid";
                    }
                    else
                    {
                        returnObj.isSuccess = false;
                        returnObj.Message = "Customer not Accepts bid";
                    }
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Customer Driver Information Not Found";
                }
            }
            else
            {
                returnObj.isSuccess = false;
                returnObj.Message = "Customer not found";
            }

            return returnObj;
        }


        [HttpPost]
        public ResponseObj<CustomerOrder> CustomerRejectBid(int CustomerOrderId, int OrderDriverId)
        {
            ResponseObj<CustomerOrder> returnObj = new ResponseObj<CustomerOrder>();
            CustomerOrder custOrder = new CustomerOrder();
            custOrder = customerorderBs.RejectBid(CustomerOrderId);
            if (custOrder != null)
            {

                //custOrder.OrderDriverId = orderDriver.OrderDriverId;
                custOrder.OrderStatusId= 1;
                   


                    if (customerorderBs.UpdateCustomerOrder(custOrder))
                    {
                        returnObj.Data = custOrder;
                        returnObj.isSuccess = true;
                        returnObj.Message = "Customer Reject Bids";
                    }
                    else
                    {
                        returnObj.isSuccess = false;
                        returnObj.Message = "Customer Reject Bids Not";
                    }
                
             
            }
            else
            {
                returnObj.isSuccess = false;
                returnObj.Message = "Customer not found";
            }

            return returnObj;
        }


        [HttpGet]

        //public ResponseObj<CustomerOrder> CustomerCancelBid(int CustomerOrderId)
        //{
        //    ResponseObj<CustomerOrder> returnObj = new ResponseObj<CustomerOrder>();
        //    CustomerOrder custOrder = new CustomerOrder();
        //    OrderStatusLog orderstatusLog = new OrderStatusLog();

        //    custOrder = customerorderBs.CancelBid(CustomerOrderId);
        //    custOrder.IsOrderFlag = true;

        //    if (customerorderBs.UpdateCustomerOrder(custOrder))
        //    {
        //        orderstatusLog.CustomerOrderId = custOrder.CustomerOrderId;
        //        orderstatusLog.OrderStatusId = 3;
        //        orderstatusLog.OrderStatusDateTime = DateTime.Now;

        //        if (orderStatusLogBs.InsertOrderStatusLog(orderstatusLog))
        //        {
        //            returnObj.Data = custOrder;
        //            returnObj.isSuccess = true;
        //            returnObj.Message = "Bid Cancel Successfully";
        //        }
        //        else
        //        {
        //            returnObj.isSuccess = false;
        //            returnObj.Message = "Bid not Cancel";
        //        }
        //    }
        //    return returnObj;
        //}


        public ResponseObj<List<OrderDriver>> GetTopBidsByDrivers(int CustomerOrderId)
        {
            ResponseObj<List<OrderDriver>> returnObj = new ResponseObj<List<OrderDriver>>();
            List<OrderDriver> orderDriver = new List<OrderDriver>();

            try
            {
                orderDriver = customerorderBs.GetMinBidPrice(CustomerOrderId);
                if (orderDriver.Count > 0)
                {
                    returnObj.Data = orderDriver;
                    returnObj.isSuccess = true;
                    returnObj.Message = "Get All Top Bids By Drivers";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "CustomerOrder,  Top 5 Bids");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }
        [HttpGet]
        public ResponseObj<List<CustomerOrder>> GetAllCustomerOrders()
        {
            ResponseObj<List<CustomerOrder>> returnObj = new ResponseObj<List<CustomerOrder>>();
            List<CustomerOrder> orderList = new List<CustomerOrder>();

            orderList = customerorderBs.GetAllCustomerOrders();
            if (orderList != null)
            {
                returnObj.Data = orderList;
                returnObj.isSuccess = true;
                returnObj.Message = "List of Customer Orders";
            }
            else
            {
                returnObj.isSuccess = false;
                returnObj.Message = "No Records found";
            }
            return returnObj;
        }
        [HttpGet]
        public ResponseObj<CustomerOrder> CancelOrder(int CustomerOrderId)
        {
            ResponseObj<CustomerOrder> returnObj = new ResponseObj<CustomerOrder>();
            CustomerOrder custOrder = new CustomerOrder();
            OrderStatusLog orderstatusLog = new OrderStatusLog();

            custOrder = customerorderBs.GetCustomerOrderById(CustomerOrderId);
            custOrder.IsOrderFlag = true;

            if (customerorderBs.UpdateCustomerOrder(custOrder))
            {
                orderstatusLog.CustomerOrderId = custOrder.CustomerOrderId;
                orderstatusLog.OrderStatusId = 3;
                orderstatusLog.OrderStatusDateTime = DateTime.Now;

                if (orderStatusLogBs.InsertOrderStatusLog(orderstatusLog))
                {
                    returnObj.Data = custOrder;
                    returnObj.isSuccess = true;
                    returnObj.Message = "Order Cancel Successfully";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Order not Cancel";
                }
            }
            return returnObj;
        }

        [HttpGet]
        public ResponseObj<CustomerOrder> OrderStatusByHero(int CustomerOrderId, int OrderStatus)
        {
            ResponseObj<CustomerOrder> returnObj = new ResponseObj<CustomerOrder>();
            CustomerOrder obj = new CustomerOrder();
            try
            {
                var cusOrder = customerorderBs.GetCustomerOrderById(CustomerOrderId);
                if (cusOrder != null)
                {
                    if (OrderStatus == 4)
                    {
                        cusOrder.OrderStatusId = 4;
                        cusOrder.DeliveryStartTime = DateTime.Now;
                    }
                    else if (OrderStatus == 5)
                    {
                        cusOrder.OrderStatusId = 5;
                        cusOrder.DeliveryEndTime = DateTime.Now;
                    }

                    if (customerorderBs.UpdateCustomerOrder(cusOrder))
                    {
                        returnObj.isSuccess = true;
                        returnObj.Data = obj;
                        returnObj.Message = "CustomerOrder tracking";
                    }
                    else
                    {
                        foreach (var error in customerorderBs.Errors)
                        {
                            returnObj.Message = error;
                            returnObj.isSuccess = false;
                            return returnObj;
                        }
                    }
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "CustomerOrder, CustomerOrderDetails");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        [HttpGet]
        public ResponseObj<List<CustomerOrderDetailsModel>> GetOrdersForHeroByStatus(string OrderDate, int HeroId, int Status)
        {
            ResponseObj<List<CustomerOrderDetailsModel>> returnObj = new ResponseObj<List<CustomerOrderDetailsModel>>();
            List<CustomerOrderDetailsModel> orderStatusList = new List<CustomerOrderDetailsModel>();

            orderStatusList = customerorderBs.GetOrdersForHeroByStatus(OrderDate, HeroId, Status);
            if (orderStatusList.Count > 0)
            {
                returnObj.Data = orderStatusList;
                returnObj.isSuccess = true;
                returnObj.Message = "List of Order Satuts";
            }
            else
            {
                returnObj.isSuccess = false;
                returnObj.Message = "No Records found";
            }
            return returnObj;
        }


        [HttpGet]
        public ResponseObj<List<CustomerOrderDetailsModel>> GetRejectedList(string OrderDate, int HeroId, int Status)
        {
            ResponseObj<List<CustomerOrderDetailsModel>> returnObj = new ResponseObj<List<CustomerOrderDetailsModel>>();
            List<CustomerOrderDetailsModel> orderStatusList = new List<CustomerOrderDetailsModel>();

            orderStatusList = customerorderBs.GetOrdersForHeroByStatus(OrderDate, HeroId, Status);
            if (orderStatusList.Count > 0)
            {
                returnObj.Data = orderStatusList;
                returnObj.isSuccess = true;
                returnObj.Message = "List of Order Satuts";
            }
            else
            {
                returnObj.isSuccess = false;
                returnObj.Message = "No Records found";
            }
            return returnObj;
        }



        [HttpGet]
        public ResponseObj<List<CustomerOrder>> MyOrders(int CustomerId)
        {
            ResponseObj<List<CustomerOrder>> returnObj = new ResponseObj<List<CustomerOrder>>();
            List<CustomerOrder> custOrder = new List<CustomerOrder>();

            custOrder = customerorderBs.GetOrderByCustomerId(CustomerId);
            if (custOrder != null)
            {
                returnObj.Data = custOrder;
                returnObj.isSuccess = true;
                returnObj.Message = "My Order List";
            }
            else
            {
                returnObj.isSuccess = false;
                returnObj.Message = "No Records Found";
            }
            return returnObj;
        }

        [HttpGet]
        public ResponseObj<CustomerOrderDetailsModel> GetCustomerOrderDetails(int CustomerOrderId)
        {
            ResponseObj<CustomerOrderDetailsModel> returnObj = new ResponseObj<CustomerOrderDetailsModel>();
            CustomerOrderDetailsModel custOrderDetails = new CustomerOrderDetailsModel();
            try
            {
                custOrderDetails = customerorderBs.GetCustomerOrderDetails(CustomerOrderId);
                if (custOrderDetails != null)
                {
                    returnObj.Data = custOrderDetails;
                    returnObj.isSuccess = true;
                    returnObj.Message = "Customer Order Details";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "CustomerOrder, CustomerOrderDetails");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        [HttpGet]
        public ResponseObj<List<CustomerOrderDetailsModel>> GetCustomerOrderList(int HeroId)
        {
            ResponseObj<List<CustomerOrderDetailsModel>> returnObj = new ResponseObj<List<CustomerOrderDetailsModel>>();
            List<CustomerOrderDetailsModel> custOrderList = new List<CustomerOrderDetailsModel>();
            try
            {
                var date = DateTime.Now;
                custOrderList = customerorderBs.GetCustomerOrderList(date, HeroId);
                returnObj.Data = custOrderList;
                returnObj.isSuccess = true;
                returnObj.Message = "Customer Order Details";
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "CustomerOrder, CustomerOrderDetails");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }


       

        [HttpGet]
        public ResponseObj<List<CustomerOrderDetailsModel>> GetcompleteCustomerOrderListByUserId(int OrderDriverId)
        {
            ResponseObj<List<CustomerOrderDetailsModel>> returnObj = new ResponseObj<List<CustomerOrderDetailsModel>>();
            List<CustomerOrderDetailsModel> custOrderList = new List<CustomerOrderDetailsModel>();
            try
            {
                var date = DateTime.Now;
                custOrderList = customerorderBs.GetcompleteCustomerOrderListByUserId(OrderDriverId);
                returnObj.Data = custOrderList;
                returnObj.isSuccess = true;
                returnObj.Message = "Customer Order Details";
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "CustomerOrder, CustomerOrderDetails");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        [HttpGet]
        public ResponseObj<OrderDriverDriverModel> TrackOrderStatus(int CustomerOrderId)
        {
            ResponseObj<OrderDriverDriverModel> returnObj = new ResponseObj<OrderDriverDriverModel>();
            OrderDriverDriverModel obj = new OrderDriverDriverModel();
            try
            {
                var OrderDriver = customerorderBs.GetCustomerOrderId(CustomerOrderId);
                if (OrderDriver == null)
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "No Records Found";
                }
                else
                {
                    var DriverInfo = customerBs.GetHeroById(OrderDriver.DriverId);
                    if (DriverInfo != null)
                    {
                        obj.OrderDriver = OrderDriver;
                        obj.Driver = DriverInfo;
                    }

                    returnObj.isSuccess = true;
                    returnObj.Data = obj;
                    returnObj.Message = "CustomerOrder tracking";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "CustomerOrder, CustomerOrderDetails");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        [HttpGet]
        public ResponseObj<List<CustomerOrder>> PendingOrders(int Status)
        {
            ResponseObj<List<CustomerOrder>> returnObj = new ResponseObj<List<CustomerOrder>>();
            List<CustomerOrder> orderList = new List<CustomerOrder>();

            orderList = customerorderBs.PendingOrders(Status);
            if (orderList.Count > 0)
            {
                returnObj.Data = orderList;
                returnObj.isSuccess = true;
                returnObj.Message = "List of Order Satuts";
            }
            else
            {
                returnObj.isSuccess = false;
                returnObj.Message = "No Records found";
            }
            return returnObj;
        }

        [HttpGet]
        public ResponseObj<List<Customer>> GetAllCustomerList()
        {
            ResponseObj<List<Customer>> returnObj = new ResponseObj<List<Customer>>();
            List<Customer> CustomerList = new List<Customer>();

            CustomerList = customerBs.GetAllCustomerList();

            if (CustomerList.Count > 0)
            {
                returnObj.Data = CustomerList;
                returnObj.isSuccess = true;
                returnObj.Message = "List of Customers";
            }
            else
            {
                returnObj.isSuccess = false;
                returnObj.Message = "No Records found";
            }
            return returnObj;
        }


     



    }


    }
