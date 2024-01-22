using System;
using System.Linq;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ExpensePaymentSystemDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ExpensePaymentSystemDbContext>>()))
        {
            // Veritabanında herhangi bir Employee kaydı var mı kontrol et
            if (!context.Employees.Any())
            {
                // Employee eklemek için örnek veri
                var employee1 = new Employee
                {
                    IdentityNumber = "12345678901",
                    FirstName = "sys",
                    LastName = "admin",
                    UserName = "admin",
                    Password = "14e1b600b1fd579f47433b88e8d85291",
                    Role = "Admin"
                };

                var employee2 = new Employee
                {
                    IdentityNumber = "98765432101",
                    FirstName = "muhammet",
                    LastName = "aksoy",
                    UserName = "muhammet",
                    Password = "ec6a6536ca304edf844d1d248a4f08dc",
                    Role = "Employee"
                };

                var employee3 = new Employee
                {
                    IdentityNumber = "12365432101",
                    FirstName = "enes",
                    LastName = "aksoy",
                    UserName = "enes",
                    Password = "ec6a6536ca304edf844d1d248a4f08dc",
                    Role = "Employee"
                };
                employee1.InsertDate = DateTime.Now;
                employee2.InsertDate = DateTime.Now;
                employee3.InsertDate = DateTime.Now;

                context.Employees.AddRange(employee1, employee2, employee3);
                context.SaveChanges();
            }

            // Category eklemek için örnek veri
            if (!context.Categorys.Any())
            {
                var category = new Category
                {
                    CategoryType = "Transportation Expenditures"
                };
                var category1 = new Category
                {
                    CategoryType = "Food Costs"
                     
                };
                var category2 = new Category
                {
                    CategoryType = "Education Expenditures"
                    
                };
                category.InsertDate = DateTime.Now;
                category1.InsertDate = DateTime.Now;
                category2.InsertDate = DateTime.Now;

                context.Categorys.AddRange(category, category1, category2);
                context.SaveChanges();
            }

            // PaymentMethod eklemek için örnek veri
            if (!context.PaymentMethods.Any())
            {
                var paymentMethod = new PaymentMethod
                {
                    PaymentMethodType = "Credit Card"
                };
                var paymentMethod1 = new PaymentMethod
                {
                    PaymentMethodType = "Cash"
                };
                var paymentMethod2 = new PaymentMethod
                {
                    PaymentMethodType = "Bank Card"
                };
                paymentMethod.InsertDate = DateTime.Now;
                paymentMethod1.InsertDate = DateTime.Now;
                paymentMethod2.InsertDate = DateTime.Now;

                context.PaymentMethods.AddRange(paymentMethod, paymentMethod1, paymentMethod2);
                context.SaveChanges();
            }
        }
    }
}
