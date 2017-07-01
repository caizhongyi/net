using System;
using System.Collections;
using System.Collections.Generic;

using FluorineFx;
using FluorineFx.AMF3;

namespace ServiceLibrary
{
	/// <summary>
	/// Fluorine sample service.
	/// </summary>
	[RemotingService("Fluorine sample service")]
	public class SampleService
	{
        public SampleService()
		{
		}

        public IList<AgreementVO> GetAgreements()
        {
            IList<AgreementVO> agreements = new List<AgreementVO>();

            AgreementVO agreement = new AgreementVO(1, DateTime.Now);
            ContactVO contact = new ContactVO(1, "First seller");
            agreement.Contact = contact;
            agreement.Assets.Add(new AssetVO(1, "Asset1"));
            agreement.Assets.Add(new AssetVO(2, "Asset2"));
            agreements.Add(agreement);

            agreement = new AgreementVO(2, DateTime.Now);
            contact = new ContactVO(2, "Second seller");
            agreement.Contact = contact;
            agreements.Add(agreement);

            return agreements;

        }

        public void ProcessAgreement(AgreementVO agreement)
        {
            AgreementVO agreementTmp = agreement;
            //Do something with agreement...
        }
	}
}
