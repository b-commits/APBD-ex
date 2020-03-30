using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.DAL;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections;

namespace WebApplication1.Controllers
{
    // Zapytanie do bazy sprawdzające, czy indeks istnieje, ale jeśli istnieje to zwrócić 400 (bad request).
    // Można też wziąć w try/catch.
    // Metoda begin transaction.
    // Commita trzeba umieścić. 
    // AddScoped (interfejs, klasa implementujaca interfejs) tak jak mockdb. Laczysz interfejs z klasa
    // go 
    // Przy wysylaniu daty urodzenia uzyc formatu ISO ("YYYY-MM-DD"). Parser jsonowy dziala na formacie ISO wlasnie.
    // Musi być DateTime w dacie, a nie string. 
    // Return Statuscode((int)HttpStatusCode.Created, obiekt enrollmentu);
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController
    {
    }
}
