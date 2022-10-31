using CommonLayer.FeedbackModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRL
    {
        AddFeedbackModel Addfeedback(AddFeedbackModel addFeedbackM);
        List<GetFeedbackModel> GetFeedback(int bookId);
    }
}
