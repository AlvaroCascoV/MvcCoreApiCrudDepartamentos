using MvcCoreApiCrudDepartamentos.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcCoreApiCrudDepartamentos.Services
{
    public class ServiceDepartamentos
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue header;
        public ServiceDepartamentos(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiCrudDepartamentos");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiASync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string request = "api/departamentos";
            List<Departamento> departamentos = await this.CallApiASync<List<Departamento>>(request);
            return departamentos;
        }

        public async Task<Departamento> FindDepartamentoAsync(int id)
        {
            string request = "api/departamentos/" + id;
            Departamento departamento = await this.CallApiASync<Departamento>(request);
            return departamento;
        }

        //LOS METODOS DE ACCION (POST, PUT, DELETE) TAMBIEN
        //PUEDEN SER GENERICOS RECIBIENDO T
        //NORMALMENTE, SI SON PETICIONES POR URL NO SE SUELE HACER GENERICO

        public async Task CreateDepartamentoAsync(int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //CREAMOS NUESTRO MODEL
                Departamento departamento = new Departamento();
                departamento.IdDepartamento = id;
                departamento.Nombre = nombre;
                departamento.Localidad = localidad;
                string json = JsonConvert.SerializeObject(departamento);
                //PARA ENVIAR INFORMACION SE UTILIZA LA CLASE
                //StringContent QUE INDICA LA INFORMACION, EL FORMATO
                //Y EL TIPO DE INFORMACION
                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdateDepartamentoAsync(int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Departamento departamento = new Departamento();
                departamento.IdDepartamento = id;
                departamento.Nombre = nombre;
                departamento.Localidad = localidad;
                string json = JsonConvert.SerializeObject(departamento);
                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeleteDepartamentoAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = $"api/departamentos/" + id;
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
    }
}
