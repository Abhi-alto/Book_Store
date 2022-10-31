using CommonLayer.FeedbackModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface IFeedbackBL
    {
        AddFeedbackModel Addfeedback(AddFeedbackModel addFeedbackM);
        List<GetFeedbackModel> GetFeedback(int bookId);
    }
}
