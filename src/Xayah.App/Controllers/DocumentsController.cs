using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xayah.App.ViewModels;
using Xayah.Business.Interface;
using Xayah.Business.Interface.Services;
using Xayah.Business.Model;

namespace Xayah.App.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public DocumentsController(IDocumentRepository documentRepository,
                                   ITransactionRepository transactionRepository,
                                   ITransactionService transactionService,
                                   IMapper mapper)
        {
            _documentRepository = documentRepository;
            _transactionRepository = transactionRepository;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        // GET: Documents
        public ActionResult Index()
        {
            return View();
        }

        // GET: Documents/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(List<IFormFile> filesUpload)
        {
            var documentViewModel = new List<DocumentViewModel>();


            foreach (var file in filesUpload)
            {
                var prefixFile = Guid.NewGuid() + "_" + file.FileName;              
               
                if(!await UploadFiles(file, prefixFile))
                {
                    return View("Create");
                }

                documentViewModel.Add(new DocumentViewModel
                {
                    UploadDocument = file,
                    FileUpload = prefixFile

                });              
            }
     
            foreach (var document in documentViewModel)
            {
                await UploadDatabase(document);
            }

            //Adding Transactions and files in Database
            List<Transaction> transactionsList = _mapper.Map<List<Transaction>>(CreateListTransactionViewModel(filesUpload));
            await _transactionRepository.RemoveAll();
            await _transactionService.TransactionList(transactionsList);

            documentViewModel.Clear();

            return RedirectToAction("Index", "Transactions");
      
        }

          

        // GET: Documents/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Documents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Documents/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<bool> UploadFiles(IFormFile file, string prefixFile)
        {
            if (file.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", prefixFile + file.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "this file already exists!");
                return false;
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

        private async Task<bool> UploadDatabase(DocumentViewModel documentViewModel)
        {
            if (!ModelState.IsValid) return false;

            await _documentRepository.Add(_mapper.Map<Document>(documentViewModel));           

            return true;
        }

        private List<TransactionViewModel> CreateListTransactionViewModel(List<IFormFile> files)
        {
            var transactions = new List<TransactionViewModel>();
            foreach (var file in files)
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    string line = "";
                    string lineValues = "";
                    int count = 0;
                    while (!stream.EndOfStream || line.Contains("</STMTTRN>"))
                    {
                        line = stream.ReadLine();
                        if (line.Contains("<TRNTYPE>") || line.Contains("<DTPOSTED>") || line.Contains("<TRNAMT>") || line.Contains("<MEMO>"))
                        {
                            lineValues += line.Split('>')[1] + ",";
                            count++;
                        }
                        if (count == 4)
                        {
                            var arrayValues = lineValues.Split(',');
                            TransactionTypeViewModel typeT = 0;
                            if (arrayValues[0].ToLower().Equals("debit")) typeT = TransactionTypeViewModel.Credit;

                            if (arrayValues[0].ToLower().Equals("credit")) typeT = TransactionTypeViewModel.Debit;

                            transactions.Add(new TransactionViewModel()
                            {
                                TRNTYPE = typeT,
                                DTPOSTED = DateTime.ParseExact(arrayValues[1].Substring(0, 8), "yyyyMMdd", null),                               
                                TRNAMT = arrayValues[2],
                                 MEMO = arrayValues[3].ToString(CultureInfo.CurrentCulture)
                            });
                            count = 0;
                            lineValues = "";
                        }
                    }
                }
            }

            return transactions;
        }

    }
}
