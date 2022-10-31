using BuisnessLayer.Interface;
using CommonLayer.FeedbackModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class FeedbackBL : IFeedbackBL
    {
        IFeedbackRL _feedbackRL;

        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            _feedbackRL = feedbackRL;
        }

        public AddFeedbackModel Addfeedback(AddFeedbackModel addFeedbackM)
        {
            try
            {
                return this._feedbackRL.Addfeedback(addFeedbackM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetFeedbackModel> GetFeedback(int bookId)
        {
            try
            {
                return this._feedbackRL.GetFeedback(bookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
