﻿using BenchmarkDotNet.Running;
using MSF.Util.Bogus;
using MSF.Util.Collections;
using MSF.Util.Core.FluentEmail;
using MSF.Util.EPPlus;
using MSF.Util.FluentValidation;
using MSF.Util.Generics;
using MSF.Util.Humanizer;
using MSF.Util.Mapper;
using MSF.Util.Markdig;
using MSF.Util.Polly;
using MSF.Util.Singleton_vs_Static;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSF.Util.Tasks;
using MSF.Util.LazyLoad;

namespace MSF.Util.Core
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var lz = new LazyDemo();
            lz.Loader(100);
            Console.ReadLine();

            // TaskDemo.Execute();
            // BenchmarkRunner.Run<MapperDemo>();
            // MapsterDemo.FastMaper();
            // PollyDemo.PollyTest();
            // var t = new NonStatic("Nome");
            // CollectionsDemo.FastestsCollections();
            // HumanizerDemo.Humanize();
            // MarkdigDemo.StringToHtml();
            // var bog = BogusDemo.GenerateCustomer();

            //var customer = new CustomerModel
            //{
            //    ID = 1,
            //    Name = "Opa",
            //    Address = "Ruaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            //};
            //var validator = new CustomerValidator();
            //var result = validator.Validate(customer);

            // var esender = new EmailSender();
            // await esender.SendEmail();

            // var teste = Generics.Generics.ReadFile<ExModel>("C:\\Users\\Falt_\\Documentos\\github\\Log\\Generic.txt");


            //var listObj = new List<ExModel>();
            //var obj1 = new ExModel
            //{
            //    Address = "Favela 1",
            //    FirstName = "Baile 1",
            //    LastName = "De 1",
            //    DataCriacao = DateTime.Today.ToString("dd/MM/yyyy")
            //};
            //var obj2 = new ExModel
            //{
            //    Address = "Favela 2",
            //    FirstName = "Baile 2",
            //    LastName = "De 2",
            //    DataCriacao = DateTime.Today.ToString("dd/MM/yyyy")
            //};
            //listObj.Add(obj1); 
            //listObj.Add(obj2);

            // var teste2 = Generics.Generics.SaveFile<ExModel>(listObj, $"C:\\Users\\Falt_\\Documentos\\github\\Log\\SaveGeneric.csv");

            // ExcelDemo.CreateExcel();
        }
    }
}
