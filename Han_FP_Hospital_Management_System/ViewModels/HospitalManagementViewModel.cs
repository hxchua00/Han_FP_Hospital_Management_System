using Han_FP_Hospital_Management_System.Interfaces;
using HospitalManagement.Common.Common;
using HospitalManagement.Common.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Han_FP_Hospital_Management_System.ViewModels
{
    class HospitalManagementViewModel : IHospitalManagementViewModel
    {
        private readonly HttpClient _hospitalManagementClient;
        internal HospitalManagementViewModel()
        {
            _hospitalManagementClient = new HttpClient();
            _hospitalManagementClient.BaseAddress = new Uri("https://localhost:44391/");
        }

        public string AddPatient(PatientDTO patient)
        {
            Task<string> responseBody;
            
            StringContent queryString = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = _hospitalManagementClient.PostAsync("api/Worker/AddPatient", queryString);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return responseBody.Result;
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        public PatientVisitRecordDTO AdmitPatient(int ID, PatientVisitRecordDTO visitRecord)
        {
            Task<string> responseBody;

            StringContent queryString = new StringContent(JsonConvert.SerializeObject(visitRecord), Encoding.UTF8, "application/json");
            var response = _hospitalManagementClient.PostAsync("api/Worker/AdmitPatient", queryString);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<PatientVisitRecordDTO>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        public BillDTO GenerateBill(int ID)
        {
            Task<string> responseBody;
            BillDTO billDTO = new BillDTO();
            StringContent queryString = new StringContent(JsonConvert.SerializeObject(billDTO), Encoding.UTF8, "application/json");
            var response = _hospitalManagementClient.PutAsync("api/Worker/GenerateBill?ID=" + ID, queryString);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<BillDTO>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        public double GetDepartmentPrice(int ID)
        {
            double deptPrice = 0;
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetDepartmentPrice?ID=" + ID);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                deptPrice = Convert.ToDouble(readTask.Result);
            }

            return deptPrice;
        }

        public double GetMedicinePrice(int ID)
        {
            double medPrice = 0;
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetMedicinePrice?ID=" + ID);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                medPrice = Convert.ToDouble(readTask.Result);
            }

            return medPrice;
        }

        public double GetWardPrice(int ID)
        {
            double wardPrice = 0;
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetWardPrice?ID=" + ID);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                wardPrice = Convert.ToDouble(readTask.Result);
            }

            return wardPrice;
        }

        public DepartmentEnum GetPatientDept(int ID)
        {
            DepartmentEnum dept = DepartmentEnum.Invalid;
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetPatientDept?ID=" + ID);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                dept = (DepartmentEnum)Enum.Parse(typeof(DepartmentEnum), readTask.Result);
            }

            return dept;
        }

        public ICollection<string> GetPatientMedicineList(int ID)
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync("api/Worker/GetPatientMeds?ID=" + ID);
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<ICollection<string>>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        public string GetPatientName(int ID)
        {
            string pName = "";
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetPatientName?ID=" + ID);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                pName = readTask.Result;
            }

            return pName;
        }

        public int GetPatientVisitRecordsCount(int ID)
        {
            int count = 0;
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetPatientVisitRecordsCount?ID=" + ID);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                count = Convert.ToInt32(readTask.Result);
            }

            return count;
        }

        public WardEnum GetPatientWard(int ID)
        {
            WardEnum ward = WardEnum.Invalid;
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetPatientDept?ID=" + ID);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                ward = (WardEnum)Enum.Parse(typeof(WardEnum), readTask.Result);
            }

            return ward;
        }

        public UserDTO GetUser(int userID)
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync("api/UserManager/GetUser?userID="+userID);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<UserDTO>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        public bool LogOn(int userId, string password)
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync($"api/UserManager/VerifyLogin?userId=" + userId + "&password=" + password);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return bool.Parse(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        public ICollection<BillDTO> PrintAllBills()
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync("api/Admin/PrintAllBills");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return null;
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        public ICollection<PatientDTO> GetPatientByDepartment(DepartmentEnum departmentType)
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync($"api/Admin/GetPatientByDepartment?departmentType=" + departmentType);
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<ICollection<PatientDTO>>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        public BillDTO SettleBill(int ID, int BillID)
        {
            Task<string> responseBody;
            var method = new HttpMethod("PATCH");
            HttpRequestMessage request = new HttpRequestMessage(method, $"api/Worker/SettleBill?ID=" + ID + "&BillID=" + BillID);
            var response = _hospitalManagementClient.SendAsync(request);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<BillDTO>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        public bool ValidatePatient(int ID)
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync("api/Worker/ValidatePatient?ID=" + ID);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return bool.Parse(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        public PatientDTO ViewPatientInfo(int ID)
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync("api/Worker/ViewPatientInfo?ID=" + ID);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<PatientDTO>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
    }
}
