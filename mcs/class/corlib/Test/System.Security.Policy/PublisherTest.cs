//
// PublisherTest.cs - NUnit Test Cases for Publisher
//
// Author:
//	Sebastien Pouliot (spouliot@motus.com)
//
// (C) 2002, 2003 Motus Technologies Inc. (http://www.motus.com)
//

using NUnit.Framework;
using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Policy;

namespace MonoTests.System.Security.Policy {

[TestFixture]
public class PublisherTest {

	static byte[] msSpCert = { 0x30, 0x82, 0x05, 0x0F, 0x30, 0x82, 0x03, 0xF7, 0xA0, 0x03, 0x02, 0x01, 0x02, 0x02, 0x0A, 0x61, 0x07, 0x11, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x34, 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x05, 0x05, 0x00, 0x30, 0x81, 0xA6, 0x31, 0x0B, 0x30, 0x09, 0x06, 0x03, 0x55, 0x04, 0x06, 0x13, 0x02, 0x55, 0x53, 0x31, 0x13, 0x30, 0x11, 0x06, 0x03, 0x55, 0x04, 0x08, 0x13, 0x0A, 0x57, 0x61, 0x73, 0x68, 0x69, 0x6E, 0x67, 0x74, 0x6F, 0x6E, 0x31, 0x10, 0x30, 0x0E, 0x06, 0x03, 0x55, 0x04, 0x07, 0x13, 0x07, 0x52, 0x65, 0x64, 0x6D, 0x6F, 0x6E, 0x64, 0x31, 0x1E, 0x30, 0x1C, 0x06, 0x03, 0x55, 0x04, 0x0A, 0x13, 0x15, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x6F, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x31, 0x2B, 0x30, 0x29, 0x06, 0x03, 0x55, 0x04, 0x0B, 0x13, 0x22, 0x43, 0x6F, 0x70, 0x79, 0x72, 0x69, 0x67, 0x68, 0x74, 0x20, 0x28, 0x63, 0x29, 0x20, 0x32, 0x30, 0x30, 0x30, 0x20, 0x4D, 0x69, 0x63, 0x72,
		0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x2E, 0x31, 0x23, 0x30, 0x21, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13, 0x1A, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x43, 0x6F, 0x64, 0x65, 0x20, 0x53, 0x69, 0x67, 0x6E, 0x69, 0x6E, 0x67, 0x20, 0x50, 0x43, 0x41, 0x30, 0x1E, 0x17, 0x0D, 0x30, 0x32, 0x30, 0x35, 0x32, 0x35, 0x30, 0x30, 0x35, 0x35, 0x34, 0x38, 0x5A, 0x17, 0x0D, 0x30, 0x33, 0x31, 0x31, 0x32, 0x35, 0x30, 0x31, 0x30, 0x35, 0x34, 0x38, 0x5A, 0x30, 0x81, 0xA1, 0x31, 0x0B, 0x30, 0x09, 0x06, 0x03, 0x55, 0x04, 0x06, 0x13, 0x02, 0x55, 0x53, 0x31, 0x13, 0x30, 0x11, 0x06, 0x03, 0x55, 0x04, 0x08, 0x13, 0x0A, 0x57, 0x61, 0x73, 0x68, 0x69, 0x6E, 0x67, 0x74, 0x6F, 0x6E, 0x31, 0x10, 0x30, 0x0E, 0x06, 0x03, 0x55, 0x04, 0x07, 0x13, 0x07, 0x52, 0x65, 0x64, 0x6D, 0x6F, 0x6E, 0x64, 0x31, 0x1E, 0x30, 0x1C, 0x06, 0x03, 0x55, 0x04, 0x0A, 0x13, 0x15, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x6F, 0x72, 0x61,
		0x74, 0x69, 0x6F, 0x6E, 0x31, 0x2B, 0x30, 0x29, 0x06, 0x03, 0x55, 0x04, 0x0B, 0x13, 0x22, 0x43, 0x6F, 0x70, 0x79, 0x72, 0x69, 0x67, 0x68, 0x74, 0x20, 0x28, 0x63, 0x29, 0x20, 0x32, 0x30, 0x30, 0x32, 0x20, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x2E, 0x31, 0x1E, 0x30, 0x1C, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13, 0x15, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x6F, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x30, 0x82, 0x01, 0x22, 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00, 0x03, 0x82, 0x01, 0x0F, 0x00, 0x30, 0x82, 0x01, 0x0A, 0x02, 0x82, 0x01, 0x01, 0x00, 0xAA, 0x99, 0xBD, 0x39, 0xA8, 0x18, 0x27, 0xF4, 0x2B, 0x3D, 0x0B, 0x4C, 0x3F, 0x7C, 0x77, 0x2E, 0xA7, 0xCB, 0xB5, 0xD1, 0x8C, 0x0D, 0xC2, 0x3A, 0x74, 0xD7, 0x93, 0xB5, 0xE0, 0xA0, 0x4B, 0x3F, 0x59, 0x5E, 0xCE, 0x45, 0x4F, 0x9A, 0x79, 0x29, 0xF1, 0x49, 0xCC, 0x1A, 0x47, 0xEE, 0x55, 0xC2, 0x08,
		0x3E, 0x12, 0x20, 0xF8, 0x55, 0xF2, 0xEE, 0x5F, 0xD3, 0xE0, 0xCA, 0x96, 0xBC, 0x30, 0xDE, 0xFE, 0x58, 0xC8, 0x27, 0x32, 0xD0, 0x85, 0x54, 0xE8, 0xF0, 0x91, 0x10, 0xBB, 0xF3, 0x2B, 0xBE, 0x19, 0xE5, 0x03, 0x9B, 0x0B, 0x86, 0x1D, 0xF3, 0xB0, 0x39, 0x8C, 0xB8, 0xFD, 0x0B, 0x1D, 0x3C, 0x73, 0x26, 0xAC, 0x57, 0x2B, 0xCA, 0x29, 0xA2, 0x15, 0x90, 0x82, 0x15, 0xE2, 0x77, 0xA3, 0x40, 0x52, 0x03, 0x8B, 0x9D, 0xC2, 0x70, 0xBA, 0x1F, 0xE9, 0x34, 0xF6, 0xF3, 0x35, 0x92, 0x4E, 0x55, 0x83, 0xF8, 0xDA, 0x30, 0xB6, 0x20, 0xDE, 0x57, 0x06, 0xB5, 0x5A, 0x42, 0x06, 0xDE, 0x59, 0xCB, 0xF2, 0xDF, 0xA6, 0xBD, 0x15, 0x47, 0x71, 0x19, 0x25, 0x23, 0xD2, 0xCB, 0x6F, 0x9B, 0x19, 0x79, 0xDF, 0x6A, 0x5B, 0xF1, 0x76, 0x05, 0x79, 0x29, 0xFC, 0xC3, 0x56, 0xCA, 0x8F, 0x44, 0x08, 0x85, 0x55, 0x8A, 0xCB, 0xC8, 0x0F, 0x46, 0x4B, 0x55, 0xCB, 0x8C, 0x96, 0x77, 0x4A, 0x87, 0xE8, 0xA9, 0x41, 0x06, 0xC7, 0xFF, 0x0D, 0xE9, 0x68, 0x57, 0x63, 0x72, 0xC3, 0x69, 0x57, 0xB4, 0x43, 0xCF, 0x32, 0x3A, 0x30, 0xDC,
		0x1B, 0xE9, 0xD5, 0x43, 0x26, 0x2A, 0x79, 0xFE, 0x95, 0xDB, 0x22, 0x67, 0x24, 0xC9, 0x2F, 0xD0, 0x34, 0xE3, 0xE6, 0xFB, 0x51, 0x49, 0x86, 0xB8, 0x3C, 0xD0, 0x25, 0x5F, 0xD6, 0xEC, 0x9E, 0x03, 0x61, 0x87, 0xA9, 0x68, 0x40, 0xC7, 0xF8, 0xE2, 0x03, 0xE6, 0xCF, 0x05, 0x02, 0x03, 0x01, 0x00, 0x01, 0xA3, 0x82, 0x01, 0x40, 0x30, 0x82, 0x01, 0x3C, 0x30, 0x0E, 0x06, 0x03, 0x55, 0x1D, 0x0F, 0x01, 0x01, 0xFF, 0x04, 0x04, 0x03, 0x02, 0x06, 0xC0, 0x30, 0x13, 0x06, 0x03, 0x55, 0x1D, 0x25, 0x04, 0x0C, 0x30, 0x0A, 0x06, 0x08, 0x2B, 0x06, 0x01, 0x05, 0x05, 0x07, 0x03, 0x03, 0x30, 0x1D, 0x06, 0x03, 0x55, 0x1D, 0x0E, 0x04, 0x16, 0x04, 0x14, 0x6B, 0xC8, 0xC6, 0x51, 0x20, 0xF0, 0xB4, 0x2F, 0xD3, 0xA0, 0xB6, 0xAE, 0x7F, 0x5E, 0x26, 0xB2, 0xB8, 0x87, 0x52, 0x29, 0x30, 0x81, 0xA9, 0x06, 0x03, 0x55, 0x1D, 0x23, 0x04, 0x81, 0xA1, 0x30, 0x81, 0x9E, 0x80, 0x14, 0x29, 0x5C, 0xB9, 0x1B, 0xB6, 0xCD, 0x33, 0xEE, 0xBB, 0x9E, 0x59, 0x7D, 0xF7, 0xE5, 0xCA, 0x2E, 0xC4, 0x0D, 0x34, 0x28, 0xA1, 0x74,
		0xA4, 0x72, 0x30, 0x70, 0x31, 0x2B, 0x30, 0x29, 0x06, 0x03, 0x55, 0x04, 0x0B, 0x13, 0x22, 0x43, 0x6F, 0x70, 0x79, 0x72, 0x69, 0x67, 0x68, 0x74, 0x20, 0x28, 0x63, 0x29, 0x20, 0x31, 0x39, 0x39, 0x37, 0x20, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x2E, 0x31, 0x1E, 0x30, 0x1C, 0x06, 0x03, 0x55, 0x04, 0x0B, 0x13, 0x15, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x6F, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x31, 0x21, 0x30, 0x1F, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13, 0x18, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x52, 0x6F, 0x6F, 0x74, 0x20, 0x41, 0x75, 0x74, 0x68, 0x6F, 0x72, 0x69, 0x74, 0x79, 0x82, 0x10, 0x6A, 0x0B, 0x99, 0x4F, 0xC0, 0x00, 0xDE, 0xAA, 0x11, 0xD4, 0xD8, 0x40, 0x9A, 0xA8, 0xBE, 0xE6, 0x30, 0x4A, 0x06, 0x03, 0x55, 0x1D, 0x1F, 0x04, 0x43, 0x30, 0x41, 0x30, 0x3F, 0xA0, 0x3D, 0xA0, 0x3B, 0x86, 0x39, 0x68, 0x74, 0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x63, 0x72, 0x6C,
		0x2E, 0x6D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2E, 0x63, 0x6F, 0x6D, 0x2F, 0x70, 0x6B, 0x69, 0x2F, 0x63, 0x72, 0x6C, 0x2F, 0x70, 0x72, 0x6F, 0x64, 0x75, 0x63, 0x74, 0x73, 0x2F, 0x43, 0x6F, 0x64, 0x65, 0x53, 0x69, 0x67, 0x6E, 0x50, 0x43, 0x41, 0x2E, 0x63, 0x72, 0x6C, 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x05, 0x05, 0x00, 0x03, 0x82, 0x01, 0x01, 0x00, 0x35, 0x23, 0xFD, 0x13, 0x54, 0xFC, 0xE9, 0xDC, 0xF0, 0xDD, 0x0C, 0x14, 0x7A, 0xFA, 0xA7, 0xB3, 0xCE, 0xFD, 0xA7, 0x3A, 0xC8, 0xBA, 0xE5, 0xE7, 0xF6, 0x03, 0xFB, 0x53, 0xDB, 0xA7, 0x99, 0xA9, 0xA0, 0x9B, 0x36, 0x9C, 0x03, 0xEB, 0x82, 0x47, 0x1C, 0x21, 0xBD, 0x14, 0xCB, 0xE7, 0x67, 0x40, 0x09, 0xC7, 0x16, 0x91, 0x02, 0x55, 0xCE, 0x43, 0x42, 0xB4, 0xCD, 0x1B, 0x5D, 0xB0, 0xF3, 0x32, 0x04, 0x3D, 0x12, 0xE5, 0x1D, 0xA7, 0x07, 0xA7, 0x8F, 0xA3, 0x7E, 0x45, 0x55, 0x76, 0x1B, 0x96, 0x95, 0x91, 0x69, 0xF0, 0xDD, 0x38, 0xF3, 0x48, 0x89, 0xEF, 0x70, 0x40, 0xB7, 0xDB, 0xB5, 0x55,
		0x80, 0xC0, 0x03, 0xC4, 0x2E, 0xB6, 0x28, 0xDC, 0x0A, 0x82, 0x0E, 0xC7, 0x43, 0xE3, 0x7A, 0x48, 0x5D, 0xB8, 0x06, 0x89, 0x92, 0x40, 0x6C, 0x6E, 0xC5, 0xDC, 0xF8, 0x9A, 0xEF, 0x0B, 0xBE, 0x21, 0x0A, 0x8C, 0x2F, 0x3A, 0xB5, 0xED, 0xA7, 0xCE, 0x71, 0x87, 0x68, 0x23, 0xE1, 0xB3, 0xE4, 0x18, 0x7D, 0xB8, 0x47, 0x01, 0xA5, 0x2B, 0xC4, 0x58, 0xCB, 0xB2, 0x89, 0x6C, 0x5F, 0xFD, 0xD3, 0x2C, 0xC4, 0x6F, 0xB8, 0x23, 0xB2, 0x0D, 0xFF, 0x3C, 0xF2, 0x11, 0x45, 0x74, 0xF2, 0x09, 0x06, 0x99, 0x18, 0xDD, 0x6F, 0xC0, 0x86, 0x01, 0x18, 0x12, 0x1D, 0x2B, 0x16, 0xAF, 0x56, 0xEF, 0x65, 0x33, 0xA1, 0xEA, 0x67, 0x4E, 0xF4, 0x4B, 0x82, 0xAB, 0xE9, 0x0F, 0xDC, 0x01, 0xFA, 0xDF, 0x60, 0x7F, 0x66, 0x47, 0x5D, 0xCB, 0x2C, 0x70, 0xCC, 0x7B, 0x4E, 0xD9, 0x06, 0xB8, 0x6E, 0x8C, 0x0C, 0xFE, 0x62, 0x1E, 0x42, 0xF9, 0x93, 0x7C, 0xA2, 0xAB, 0x0A, 0x9E, 0xD0, 0x23, 0x10, 0xAE, 0x4D, 0x7B, 0x27, 0x91, 0x6F, 0x26, 0xBE, 0x68, 0xFA, 0xA6, 0x3F, 0x9F, 0x23, 0xEB, 0xC8, 0x9D, 0xBB, 0x87 };

	[Test]
	[ExpectedException (typeof (ArgumentNullException))]
	public void NullConstructor () 
	{
		Publisher p = new Publisher (null);
	}

	[Test]
	public void EmptyCertificateConstructor () 
	{
		byte[] n = null;
		X509Certificate x509 = new X509Certificate (n);
		Publisher p = new Publisher (x509);

		Assertion.AssertEquals ("GetHashCode", x509.GetHashCode (), p.GetHashCode ());
		Assertion.AssertEquals ("ToString", "<System.Security.Policy.Publisher version=\"1\">\r\n   <X509v3Certificate/>\r\n</System.Security.Policy.Publisher>\r\n", p.ToString ());
	}

	[Test]
	[ExpectedException (typeof (NullReferenceException))]
	public void EmptyCertificateConstructor2 () 
	{
		byte[] n = null;
		X509Certificate x509 = new X509Certificate (n);
		Publisher p = new Publisher (x509);
		x509 = p.Certificate;
	}

	[Test]
	public void Constructor () 
	{
		X509Certificate x509 = new X509Certificate (msSpCert);
		Publisher p = new Publisher (x509);

		Assertion.AssertEquals ("GetHashCode", x509.GetHashCode (), p.GetHashCode ());

		IPermission ip = p.CreateIdentityPermission (null);
		Assertion.Assert ("CreateIdentityPermission", (ip is PublisherIdentityPermission));

		string s = "<System.Security.Policy.Publisher version=\"1\">\r\n   <X509v3Certificate>3082050F308203F7A003020102020A61071143000000000034300D06092A864886F70D01010505003081A6310B3009060355040613025553311330110603550408130A57617368696E67746F6E3110300E060355040713075265646D6F6E64311E301C060355040A13154D6963726F736F667420436F72706F726174696F6E312B3029060355040B1322436F70797269676874202863292032303030204D6963726F736F667420436F72702E312330210603550403131A4D6963726F736F667420436F6465205369676E696E6720504341301E170D3032303532353030353534385A170D3033313132353031303534385A3081A1310B3009060355040613025553311330110603550408130A57617368696E67746F6E3110300E060355040713075265646D6F6E64311E301C060355040A13154D6963726F736F667420436F72706F726174696F6E312B3029060355040B1322436F70797269676874202863292032303032204D6963726F736F667420436F72702E311E301C060355040313154D6963726F736F667420436F72706F726174696F6E30820122300D06092A864886F70D01010105000382010F003082010A0282010100AA99BD39A81827F42B3D0B4C3F";
		s += "7C772EA7CBB5D18C0DC23A74D793B5E0A04B3F595ECE454F9A7929F149CC1A47EE55C2083E1220F855F2EE5FD3E0CA96BC30DEFE58C82732D08554E8F09110BBF32BBE19E5039B0B861DF3B0398CB8FD0B1D3C7326AC572BCA29A215908215E277A34052038B9DC270BA1FE934F6F335924E5583F8DA30B620DE5706B55A4206DE59CBF2DFA6BD154771192523D2CB6F9B1979DF6A5BF176057929FCC356CA8F440885558ACBC80F464B55CB8C96774A87E8A94106C7FF0DE968576372C36957B443CF323A30DC1BE9D543262A79FE95DB226724C92FD034E3E6FB514986B83CD0255FD6EC9E036187A96840C7F8E203E6CF050203010001A38201403082013C300E0603551D0F0101FF0404030206C030130603551D25040C300A06082B06010505070303301D0603551D0E041604146BC8C65120F0B42FD3A0B6AE7F5E26B2B88752293081A90603551D230481A130819E8014295CB91BB6CD33EEBB9E597DF7E5CA2EC40D3428A174A4723070312B3029060355040B1322436F70797269676874202863292031393937204D6963726F736F667420436F72702E311E301C060355040B13154D6963726F736F667420436F72706F726174696F6E3121301F060355040313184D6963726F736F667420526F6F7420417574686F7269747982106A0B994FC000DEAA11D4D8";
		s += "409AA8BEE6304A0603551D1F04433041303FA03DA03B8639687474703A2F2F63726C2E6D6963726F736F66742E636F6D2F706B692F63726C2F70726F64756374732F436F64655369676E5043412E63726C300D06092A864886F70D010105050003820101003523FD1354FCE9DCF0DD0C147AFAA7B3CEFDA73AC8BAE5E7F603FB53DBA799A9A09B369C03EB82471C21BD14CBE7674009C716910255CE4342B4CD1B5DB0F332043D12E51DA707A78FA37E4555761B96959169F0DD38F34889EF7040B7DBB55580C003C42EB628DC0A820EC743E37A485DB8068992406C6EC5DCF89AEF0BBE210A8C2F3AB5EDA7CE71876823E1B3E4187DB84701A52BC458CBB2896C5FFDD32CC46FB823B20DFF3CF2114574F209069918DD6FC0860118121D2B16AF56EF6533A1EA674EF44B82ABE90FDC01FADF607F66475DCB2C70CC7B4ED906B86E8C0CFE621E42F9937CA2AB0A9ED02310AE4D7B27916F26BE68FAA63F9F23EBC89DBB87</X509v3Certificate>\r\n</System.Security.Policy.Publisher>\r\n";
		Assertion.AssertEquals ("ToString", s, p.ToString ());
	}

	[Test]
	public void Copy () 
	{
		X509Certificate x509 = new X509Certificate (msSpCert);
		Publisher p = new Publisher (x509);
		Publisher pCopy = (Publisher) p.Copy ();

		Assertion.AssertNotNull ("Copy-Cert", pCopy.Certificate);
		Assertion.Assert("Copy-Equals", p.Equals (pCopy));
		Assertion.AssertEquals ("Copy-GetHashCode", p.GetHashCode (), pCopy.GetHashCode ());
		Assertion.AssertEquals ("Copy-ToString", p.ToString (), pCopy.ToString ());
	}
}

}