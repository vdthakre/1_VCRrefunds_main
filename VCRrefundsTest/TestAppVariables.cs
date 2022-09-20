using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;

namespace VCRrefunds
{	
	[TestFixture]
	public class TestAppVariables
	{
		[Test]
		public void testAppVariablersProperties()
		{
			VCRrefunds.RefundAppVariables test = new RefundAppVariables();

			Assert.AreEqual(test.GroupName, "ImageRes");
			Assert.AreEqual(test.QueueName, "ETTS");
			Assert.AreEqual(test.UserPassword, "R0b0tic");
			Assert.AreEqual(test.UserID, "6007");
		}

	}
}
