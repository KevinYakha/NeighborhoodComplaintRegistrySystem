﻿using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;

using NCRS_API.Data;

namespace NCRS_Client
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Page
    {
        private readonly Client _httpClient = new();
        private List<Tuple<TextBlock, HttpStatusCode>> statusLine = [];

        public Overview()
        {
            InitializeComponent();
            statusLine.Add(new(tb_loading_content, HttpStatusCode.Processing));
            statusLine.Add(new(tb_loading_failure, HttpStatusCode.NoContent));
            statusLine.Add(new(tb_loading_timeout, HttpStatusCode.RequestTimeout));
            CreateComplaintTableData();
        }

        public Overview(Tenant issuer)
        {
            InitializeComponent();
            statusLine.Add(new(tb_loading_content, HttpStatusCode.Processing));
            statusLine.Add(new(tb_loading_failure, HttpStatusCode.NoContent));
            statusLine.Add(new(tb_loading_timeout, HttpStatusCode.RequestTimeout));
            CreateComplaintTableData(issuer);
        }

        public Overview(DateTime date)
        {
            InitializeComponent();
            statusLine.Add(new(tb_loading_content, HttpStatusCode.Processing));
            statusLine.Add(new(tb_loading_failure, HttpStatusCode.NoContent));
            statusLine.Add(new(tb_loading_timeout, HttpStatusCode.RequestTimeout));
            CreateComplaintTableData(date);
        }

        public Overview(DateTime date, Tenant issuer)
        {
            InitializeComponent();
            statusLine.Add(new(tb_loading_content, HttpStatusCode.Processing));
            statusLine.Add(new(tb_loading_failure, HttpStatusCode.NoContent));
            statusLine.Add(new(tb_loading_timeout, HttpStatusCode.RequestTimeout));
            CreateComplaintTableData(date, issuer);
        }

        public Overview(Tuple<DateTime, DateTime> dateRange)
        {
            InitializeComponent();
            statusLine.Add(new(tb_loading_content, HttpStatusCode.Processing));
            statusLine.Add(new(tb_loading_failure, HttpStatusCode.NoContent));
            statusLine.Add(new(tb_loading_timeout, HttpStatusCode.RequestTimeout));
            CreateComplaintTableData(dateRange);
        }

        public Overview(Tuple<DateTime, DateTime> dateRange, Tenant issuer)
        {
            InitializeComponent();
            statusLine.Add(new(tb_loading_content, HttpStatusCode.Processing));
            statusLine.Add(new(tb_loading_failure, HttpStatusCode.NoContent));
            statusLine.Add(new(tb_loading_timeout, HttpStatusCode.RequestTimeout));
            CreateComplaintTableData(dateRange, issuer);
        }


        public static void AdjustStatusLine(List<Tuple<TextBlock, HttpStatusCode>> textBlockStatusTuple, HttpStatusCode statusCode)
        {
            foreach (Tuple<TextBlock, HttpStatusCode> statusTuple in textBlockStatusTuple)
            {
                if (statusTuple.Item2 == statusCode)
                {
                    statusTuple.Item1.Visibility = Visibility.Visible;
                }
                else
                {
                    statusTuple.Item1.Visibility = Visibility.Hidden;
                }
            }
        }

        private async void CreateComplaintTableData()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.RetrieveAllComplaints();

                if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
                {
                    AdjustStatusLine(statusLine, response.StatusCode);
                    return;
                }

                List<Complaint> complaints = await response.Content.ReadFromJsonAsync<List<Complaint>>();
                ic_complaint_entry.ItemsSource = complaints;
                AdjustStatusLine(statusLine, response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void CreateComplaintTableData(Tenant issuer)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.RetrieveComplaintsByName(issuer);

                if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
                {
                    AdjustStatusLine(statusLine, response.StatusCode);
                    return;
                }

                List<Complaint> complaints = await response.Content.ReadFromJsonAsync<List<Complaint>>();
                ic_complaint_entry.ItemsSource = complaints;
                AdjustStatusLine(statusLine, response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void CreateComplaintTableData(DateTime date)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.RetrieveComplaintsByDate(date);

                if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
                {
                    AdjustStatusLine(statusLine, response.StatusCode);
                    return;
                }

                List<Complaint> complaints = await response.Content.ReadFromJsonAsync<List<Complaint>>();
                ic_complaint_entry.ItemsSource = complaints;
                AdjustStatusLine(statusLine, response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void CreateComplaintTableData(DateTime date, Tenant issuer)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.RetrieveComplaintsByDateAndName(date, issuer);

                if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
                {
                    AdjustStatusLine(statusLine, response.StatusCode);
                    return;
                }

                List<Complaint> complaints = await response.Content.ReadFromJsonAsync<List<Complaint>>();
                ic_complaint_entry.ItemsSource = complaints;
                AdjustStatusLine(statusLine, response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void CreateComplaintTableData(Tuple<DateTime, DateTime> dateRange)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.RetrieveComplaintsByDateRange(dateRange);

                if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
                {
                    AdjustStatusLine(statusLine, response.StatusCode);
                    return;
                }

                List<Complaint> complaints = await response.Content.ReadFromJsonAsync<List<Complaint>>();
                ic_complaint_entry.ItemsSource = complaints;
                AdjustStatusLine(statusLine, response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void CreateComplaintTableData(Tuple<DateTime, DateTime> dateRange, Tenant issuer)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.RetrieveComplaintsByDateRangeAndName(dateRange, issuer);

                if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
                {
                    AdjustStatusLine(statusLine, response.StatusCode);
                    return;
                }

                List<Complaint> complaints = await response.Content.ReadFromJsonAsync<List<Complaint>>();
                ic_complaint_entry.ItemsSource = complaints;
                AdjustStatusLine(statusLine, response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
