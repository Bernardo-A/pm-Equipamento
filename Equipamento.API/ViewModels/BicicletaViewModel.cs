﻿namespace Equipamento.API.ViewModels
{
    public class BicicletaViewModel : BicicletaInsertViewModel
    {   
        public int Id { get; set; } 
    }
    public class BicicletaInsertViewModel
    {
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Ano { get; set; }
        public string? Numero { get; set; }
        public string? Status { get; set; }
    }
}
