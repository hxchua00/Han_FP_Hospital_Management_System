using Han_FP_Hospital_Management_System.Interfaces;
using HospitalManagement.Common.Common;
using HospitalManagement.Common.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Han_FP_Hospital_Management_System.ViewModels
{
    class HospitalManagementViewModel : IHospitalManagementViewModel
    {
        private readonly HttpClient _hospitalManagementClient;
        internal HospitalManagementViewModel()
        {
            _hospitalManagementClient = new HttpClient();
            _hospitalManagementClient.BaseAddress = new Uri("https://localhost:44391/api");
            Initialize();
        }
        public void Initialize()
        {
            
        }
        public PatientDTO AddPatient(PatientDTO patient)
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync("api/Worker/AddPatient");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Patient added successfully!");
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

        public PatientVisitRecordDTO AdmitPatient(int ID, PatientVisitRecordDTO visitRecord)
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync("api/Worker/AdmitPatient");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Patient admitted successfully!");
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

        public double CalculateTotalBill(int ID)
        {
            double totalAmt = 0;
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/CalculateTotalBill/" + ID);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                totalAmt = Convert.ToDouble(readTask.Result);
            }

            return totalAmt;
        }

        public PatientDTO DischargePatient(int ID, int BillID)
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync("api/Worker/DischargePatient");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Patient discharged successfully!");
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

        public double GetDepartmentPrice(int ID)
        {
            double deptPrice = 0;
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetDepartmentPrice/" + ID);
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
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetMedicinePrice/" + ID);
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
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetWardPrice/" + ID);
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
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetPatientDept/" + ID);
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
            var response = _hospitalManagementClient.GetAsync("api/Worker/GetPatientMeds");
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
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetPatientName/" + ID);
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

        public WardEnum GetPatientWard(int ID)
        {
            WardEnum ward = WardEnum.Invalid;
            var responseTask = _hospitalManagementClient.GetAsync("api/Worker/GetPatientDept/" + ID);
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
            var response = _hospitalManagementClient.GetAsync("api/UserManager/GetUser");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Patient added successfully!");
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
            var response = _hospitalManagementClient.GetAsync($"api/UserManager/VerifyLogin");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Login success!");
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
                Console.WriteLine("Patient added successfully!");
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

        public ICollection<PatientDTO> PrintAllDepartments()
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync("api/Admin/PrintAllDepartments");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Patient added successfully!");
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

        public BillDTO SettleBill(int ID, int BillID)
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync("api/Worker/PayBill");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Patient added successfully!");
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

        public bool ValidatePatient(int ID)
        {
            Task<string> responseBody;
            var response = _hospitalManagementClient.GetAsync("api/Worker/ValidatePatient");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Patient found!");
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
            var response = _hospitalManagementClient.GetAsync("api/Worker/ViewPatientInfo");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Patient added successfully!");
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
