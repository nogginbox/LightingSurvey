﻿using LightingSurvey.Data.Models;
using LightingSurvey.Data.Repositories;
using LightingSurvey.MvcSite.ActionFilters;
using LightingSurvey.MvcSite.ViewModels.Survey;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LightingSurvey.MvcSite.Controllers
{
    public class SurveyController : Controller
    {
        /// <summary>
        /// Available to all actions and retrieved using GetSurveyResponceAttribute
        /// </summary>
        public SurveyResponse CurrentResponse { get; set; }

        private readonly ISurveyResponseRepository _surveyResponseRepository;
        private const string tempResponseId = "temp-id";

        public SurveyController(ISurveyResponseRepository surveyResponseRepository)
        {
            _surveyResponseRepository = surveyResponseRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Start()
        {
            CurrentResponse = await _surveyResponseRepository.Find(tempResponseId);
            if(CurrentResponse == null)
            {
                CurrentResponse = await _surveyResponseRepository.Create();
                // todo: save external ID in cookie
                await _surveyResponseRepository.SaveChanges();
            }

            return RedirectToAction("Question1");
        }

        [ServiceFilter(typeof(GetCurrentResponceAttribute))]
        public IActionResult Question1()
        {
            return QuestionView<NameQuestionViewModel, string>(CurrentResponse.Respondent.Name);
        }

        [HttpPost]
        [CheckValidAnswer(typeof(string))]
        [ServiceFilter(typeof(GetCurrentResponceAttribute))]
        public async Task<IActionResult> Question1(NameQuestionViewModel question)
        {
            CurrentResponse.Respondent.Name = question.Answer;
            await _surveyResponseRepository.SaveChanges();

            return RedirectToAction("Question2");
        }


        [ServiceFilter(typeof(GetCurrentResponceAttribute))]
        public IActionResult Question2()
        {
            return QuestionView<EmailQuestionViewModel, string>(CurrentResponse.Respondent.EmailAddress);
        }

        [HttpPost]
        [CheckValidAnswer(typeof(string))]
        [ServiceFilter(typeof(GetCurrentResponceAttribute))]
        public async Task<IActionResult> Question2(EmailQuestionViewModel question)
        {
            CurrentResponse.Respondent.EmailAddress = question.Answer;
            await _surveyResponseRepository.SaveChanges();

            return RedirectToAction("Question3");
        }

        [ServiceFilter(typeof(GetCurrentResponceAttribute))]
        public IActionResult Question3()
        {
            return QuestionView<NameQuestionViewModel, string>(CurrentResponse.Respondent.Address.PostCode);
        }

        [HttpPost]
        [CheckValidAnswer(typeof(string))]
        [ServiceFilter(typeof(GetCurrentResponceAttribute))]
        public async Task<IActionResult> Question3(NameQuestionViewModel question)
        {
            CurrentResponse.Respondent.Address.PostCode = question.Answer;
            await _surveyResponseRepository.SaveChanges();

            return RedirectToAction("Question4");
        }

        [ServiceFilter(typeof(GetCurrentResponceAttribute))]
        public IActionResult Question4()
        {
            return QuestionView<BooleanQuestionViewModel, bool?>(CurrentResponse.HappyWithLighting);
        }

        [HttpPost]
        [CheckValidAnswer(typeof(bool))]
        [ServiceFilter(typeof(GetCurrentResponceAttribute))]
        public async Task<IActionResult> Question4(BooleanQuestionViewModel question)
        {
            CurrentResponse.HappyWithLighting = question.Answer;
            await _surveyResponseRepository.SaveChanges();

            return RedirectToAction("Question5");
        }

        [ServiceFilter(typeof(GetCurrentResponceAttribute))]
        public IActionResult Question5()
        {
            return View();
        }

        [ServiceFilter(typeof(GetCurrentResponceAttribute))]
        public IActionResult Summary()
        {
            return View();
        }

        private IActionResult QuestionView<TQuestion, TAnswer>(TAnswer answer) where TQuestion : IQuestionViewModel<TAnswer>, new()
        {
            var model = new QuestionPageViewModel<TAnswer>
            {
                Question = new TQuestion { Answer = answer }
            };

            return View(model);
        }
    }
}
