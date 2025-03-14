using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs.wallet {
   public class multyaddress : GXWebComponent
   {
      public multyaddress( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
      }

      public multyaddress( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( )
      {
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      public override void SetPrefix( string sPPrefix )
      {
         sPrefix = sPPrefix;
      }

      protected override void createObjects( )
      {
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            if ( nGotPars == 0 )
            {
               entryPointCalled = false;
               gxfirstwebparm = GetNextPar( );
               gxfirstwebparm_bkp = gxfirstwebparm;
               gxfirstwebparm = DecryptAjaxCall( gxfirstwebparm);
               toggleJsOutput = isJsOutputEnabled( );
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
               if ( StringUtil.StrCmp(gxfirstwebparm, "dyncall") == 0 )
               {
                  setAjaxCallMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  dyncall( GetNextPar( )) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "dyncomponent") == 0 )
               {
                  setAjaxEventMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  nDynComponent = 1;
                  sCompPrefix = GetPar( "sCompPrefix");
                  sSFPrefix = GetPar( "sSFPrefix");
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix});
                  componentstart();
                  context.httpAjaxContext.ajax_rspStartCmp(sPrefix);
                  componentdraw();
                  context.httpAjaxContext.ajax_rspEndCmp();
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxEvt") == 0 )
               {
                  setAjaxEventMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetNextPar( );
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetNextPar( );
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Addressgrid") == 0 )
               {
                  gxnrAddressgrid_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Addressgrid") == 0 )
               {
                  gxgrAddressgrid_refresh_invoke( ) ;
                  return  ;
               }
               else
               {
                  if ( ! IsValidAjaxCall( false) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = gxfirstwebparm_bkp;
               }
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      protected void gxnrAddressgrid_newrow_invoke( )
      {
         nRC_GXsfl_27 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_27"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_27_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_27_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_27_idx = GetPar( "sGXsfl_27_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrAddressgrid_newrow( ) ;
         /* End function gxnrAddressgrid_newrow_invoke */
      }

      protected void gxgrAddressgrid_refresh_invoke( )
      {
         subAddressgrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subAddressgrid_Rows"), "."), 18, MidpointRounding.ToEven));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV31wallet);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV23SDTAddressHistory);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrAddressgrid_refresh( subAddressgrid_Rows, AV31wallet, AV23SDTAddressHistory, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrAddressgrid_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               ValidateSpaRequest();
            }
            PA092( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavRecevingaddress_Enabled = 0;
               AssignProp(sPrefix, false, edtavRecevingaddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRecevingaddress_Enabled), 5, 0), true);
               edtavTotalbalance_Enabled = 0;
               AssignProp(sPrefix, false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
               edtavTransactionscount_Enabled = 0;
               AssignProp(sPrefix, false, edtavTransactionscount_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTransactionscount_Enabled), 5, 0), true);
               edtavCtlreceiveddatetime_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlreceiveddatetime_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlreceiveddatetime_Enabled), 5, 0), !bGXsfl_27_Refreshing);
               edtavGridrecevingaddress_Enabled = 0;
               AssignProp(sPrefix, false, edtavGridrecevingaddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGridrecevingaddress_Enabled), 5, 0), !bGXsfl_27_Refreshing);
               edtavCtlreceivedamount_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlreceivedamount_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlreceivedamount_Enabled), 5, 0), !bGXsfl_27_Refreshing);
               edtavCtlreceivedtransactionid_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlreceivedtransactionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlreceivedtransactionid_Enabled), 5, 0), !bGXsfl_27_Refreshing);
               edtavFirstlink_Enabled = 0;
               AssignProp(sPrefix, false, edtavFirstlink_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavFirstlink_Enabled), 5, 0), !bGXsfl_27_Refreshing);
               edtavCtlsentdatetime_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlsentdatetime_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlsentdatetime_Enabled), 5, 0), !bGXsfl_27_Refreshing);
               edtavCtlsenttransactionid_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlsenttransactionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlsenttransactionid_Enabled), 5, 0), !bGXsfl_27_Refreshing);
               edtavSecondlink_Enabled = 0;
               AssignProp(sPrefix, false, edtavSecondlink_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSecondlink_Enabled), 5, 0), !bGXsfl_27_Refreshing);
               edtavCtlbalance_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlbalance_Enabled), 5, 0), !bGXsfl_27_Refreshing);
               edtavTxtconfirmations_Enabled = 0;
               AssignProp(sPrefix, false, edtavTxtconfirmations_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTxtconfirmations_Enabled), 5, 0), !bGXsfl_27_Refreshing);
               WS092( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  if ( nDynComponent == 0 )
                  {
                     throw new System.Net.WebException("WebComponent is not allowed to run") ;
                  }
               }
            }
            if ( ( GxWebError == 0 ) && context.isAjaxRequest( ) )
            {
               enableOutput();
               if ( ! context.isAjaxRequest( ) )
               {
                  context.GX_webresponse.AppendHeader("Cache-Control", "no-store");
               }
               if ( ! context.WillRedirect( ) )
               {
                  AddString( context.getJSONResponse( )) ;
               }
               else
               {
                  if ( context.isAjaxRequest( ) )
                  {
                     disableOutput();
                  }
                  RenderHtmlHeaders( ) ;
                  context.Redirect( context.wjLoc );
                  context.DispatchAjaxCommands();
               }
            }
         }
         cleanup();
      }

      protected void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            context.WriteHtmlText( "<title>") ;
            context.SendWebValue( "Multy Address") ;
            context.WriteHtmlTextNl( "</title>") ;
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
            if ( StringUtil.Len( sDynURL) > 0 )
            {
               context.WriteHtmlText( "<BASE href=\""+sDynURL+"\" />") ;
            }
            define_styles( ) ;
         }
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1218140), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1218140), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1218140), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1218140), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1218140), false, true);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 1218140), false, true);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.CloseHtmlHeader();
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
            FormProcess = " data-HasEnter=\"false\" data-Skiponenter=\"false\"";
            context.WriteHtmlText( "<body ") ;
            if ( StringUtil.StrCmp(context.GetLanguageProperty( "rtl"), "true") == 0 )
            {
               context.WriteHtmlText( " dir=\"rtl\" ") ;
            }
            bodyStyle = "";
            if ( nGXWrapped == 0 )
            {
               bodyStyle += "-moz-opacity:0;opacity:0;";
            }
            context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.multyaddress.aspx") +"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp(sPrefix, false, "FORM", "Class", "form-horizontal Form", true);
         }
         else
         {
            bool toggleHtmlOutput = isOutputEnabled( );
            if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableOutput();
               }
            }
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            if ( toggleHtmlOutput )
            {
               if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableOutput();
                  }
               }
            }
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWALLET", AV31wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWALLET", AV31wallet);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWALLET", GetSecureSignedToken( sPrefix, AV31wallet, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Sdtaddresshistory", AV23SDTAddressHistory);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Sdtaddresshistory", AV23SDTAddressHistory);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_27", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_27), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWALLET", AV31wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWALLET", AV31wallet);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWALLET", GetSecureSignedToken( sPrefix, AV31wallet, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vSDTADDRESSHISTORY", AV23SDTAddressHistory);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vSDTADDRESSHISTORY", AV23SDTAddressHistory);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vPOPUPNAME", StringUtil.RTrim( AV19PopupName));
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSGRID_nEOF), 1, 0, ".", "")));
      }

      protected void RenderHtmlCloseForm092( )
      {
         SendCloseFormHiddens( ) ;
         if ( ( StringUtil.Len( sPrefix) != 0 ) && ( context.isAjaxRequest( ) || context.isSpaRequest( ) ) )
         {
            componentjscripts();
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GX_FocusControl", GX_FocusControl);
         define_styles( ) ;
         SendSecurityToken(sPrefix);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            SendAjaxEncryptionKey();
            SendComponentObjects();
            SendServerCommands();
            SendState();
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
            context.WriteHtmlTextNl( "</form>") ;
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            include_jscripts( ) ;
            context.WriteHtmlTextNl( "</body>") ;
            context.WriteHtmlTextNl( "</html>") ;
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
         }
         else
         {
            SendWebComponentState();
            context.WriteHtmlText( "</div>") ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
      }

      public override string GetPgmname( )
      {
         return "Wallet.MultyAddress" ;
      }

      public override string GetPgmdesc( )
      {
         return "Multy Address" ;
      }

      protected void WB090( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               RenderHtmlHeaders( ) ;
            }
            RenderHtmlOpenForm( ) ;
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.multyaddress.aspx");
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavRecevingaddress_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavRecevingaddress_Internalname, "Receving Address", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'" + sPrefix + "',false,'" + sGXsfl_27_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavRecevingaddress_Internalname, StringUtil.RTrim( AV32recevingAddress), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", 0, 1, edtavRecevingaddress_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+sPrefix+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/MultyAddress.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbqrcodeaddress_Internalname, lblTbqrcodeaddress_Caption, "", "", lblTbqrcodeaddress_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_Wallet/MultyAddress.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTotalbalance_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTotalbalance_Internalname, "Balance on wallet:", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'" + sPrefix + "',false,'" + sGXsfl_27_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTotalbalance_Internalname, StringUtil.LTrim( StringUtil.NToC( AV27totalBalance, 18, 8, ".", "")), StringUtil.LTrim( ((edtavTotalbalance_Enabled!=0) ? context.localUtil.Format( AV27totalBalance, "ZZZZZZZZ9.99999999") : context.localUtil.Format( AV27totalBalance, "ZZZZZZZZ9.99999999"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,15);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTotalbalance_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavTotalbalance_Enabled, 0, "text", "", 18, "chr", 1, "row", 18, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/MultyAddress.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTransactionscount_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTransactionscount_Internalname, "Number of Transaccions", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'" + sPrefix + "',false,'" + sGXsfl_27_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTransactionscount_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV29transactionsCount), 10, 0, ".", "")), StringUtil.LTrim( ((edtavTransactionscount_Enabled!=0) ? context.localUtil.Format( (decimal)(AV29transactionsCount), "ZZZZZZZZZ9") : context.localUtil.Format( (decimal)(AV29transactionsCount), "ZZZZZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,19);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTransactionscount_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavTransactionscount_Enabled, 0, "text", "1", 10, "chr", 1, "row", 10, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/MultyAddress.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSend_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(27), 2, 0)+","+"null"+");", "Send", bttSend_Jsonclick, 7, "Send", "", StyleString, ClassString, bttSend_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e11091_client"+"'", TempTags, "", 2, "HLP_Wallet/MultyAddress.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttUpdatebalance_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(27), 2, 0)+","+"null"+");", "Update Balance", bttUpdatebalance_Jsonclick, 5, "Update Balance", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'UPDATE BALANCE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/MultyAddress.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            AddressgridContainer.SetWrapped(nGXWrapped);
            StartGridControl27( ) ;
         }
         if ( wbEnd == 27 )
         {
            wbEnd = 0;
            nRC_GXsfl_27 = (int)(nGXsfl_27_idx-1);
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AddressgridContainer.AddObjectProperty("ADDRESSGRID_nEOF", ADDRESSGRID_nEOF);
               AddressgridContainer.AddObjectProperty("ADDRESSGRID_nFirstRecordOnPage", ADDRESSGRID_nFirstRecordOnPage);
               AV48GXV1 = nGXsfl_27_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"AddressgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Addressgrid", AddressgridContainer, subAddressgrid_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"AddressgridContainerData", AddressgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"AddressgridContainerData"+"V", AddressgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"AddressgridContainerData"+"V"+"\" value='"+AddressgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 27 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( AddressgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AddressgridContainer.AddObjectProperty("ADDRESSGRID_nEOF", ADDRESSGRID_nEOF);
                  AddressgridContainer.AddObjectProperty("ADDRESSGRID_nFirstRecordOnPage", ADDRESSGRID_nFirstRecordOnPage);
                  AV48GXV1 = nGXsfl_27_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"AddressgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Addressgrid", AddressgridContainer, subAddressgrid_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"AddressgridContainerData", AddressgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"AddressgridContainerData"+"V", AddressgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"AddressgridContainerData"+"V"+"\" value='"+AddressgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START092( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.isSpaRequest( ) )
            {
               if ( context.ExposeMetadata( ) )
               {
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_10-184260", 0) ;
               }
            }
            Form.Meta.addItem("description", "Multy Address", 0) ;
            context.wjLoc = "";
            context.nUserReturn = 0;
            context.wbHandled = 0;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               sXEvt = cgiGet( "_EventName");
               if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
               {
               }
            }
         }
         wbErr = false;
         if ( ( StringUtil.Len( sPrefix) == 0 ) || ( nDraw == 1 ) )
         {
            if ( nDoneStart == 0 )
            {
               STRUP090( ) ;
            }
         }
      }

      protected void WS092( )
      {
         START092( ) ;
         EVT092( ) ;
      }

      protected void EVT092( )
      {
         sXEvt = cgiGet( "_EventName");
         if ( ( ( ( StringUtil.Len( sPrefix) == 0 ) ) || ( StringUtil.StringSearch( sXEvt, sPrefix, 1) > 0 ) ) && ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               if ( context.wbHandled == 0 )
               {
                  if ( StringUtil.Len( sPrefix) == 0 )
                  {
                     sEvt = cgiGet( "_EventName");
                     EvtGridId = cgiGet( "_EventGridId");
                     EvtRowId = cgiGet( "_EventRowId");
                  }
                  if ( StringUtil.Len( sEvt) > 0 )
                  {
                     sEvtType = StringUtil.Left( sEvt, 1);
                     sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP090( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'UPDATE BALANCE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP090( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Update Balance' */
                                    E12092 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP090( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E13092 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP090( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtlreceiveddatetime_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ADDRESSGRIDPAGING") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP090( ) ;
                              }
                              sEvt = cgiGet( sPrefix+"ADDRESSGRIDPAGING");
                              if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                              {
                                 subaddressgrid_firstpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                              {
                                 subaddressgrid_previouspage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                              {
                                 subaddressgrid_nextpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                              {
                                 subaddressgrid_lastpage( ) ;
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "ADDRESSGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'GET KEY INFO'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'GET KEY INFO'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP090( ) ;
                              }
                              nGXsfl_27_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_27_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_27_idx), 4, 0), 4, "0");
                              SubsflControlProps_272( ) ;
                              AV48GXV1 = (int)(nGXsfl_27_idx+ADDRESSGRID_nFirstRecordOnPage);
                              if ( ( AV23SDTAddressHistory.Count >= AV48GXV1 ) && ( AV48GXV1 > 0 ) )
                              {
                                 AV23SDTAddressHistory.CurrentItem = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1));
                                 AV47gridRecevingAddress = cgiGet( edtavGridrecevingaddress_Internalname);
                                 AssignAttri(sPrefix, false, edtavGridrecevingaddress_Internalname, AV47gridRecevingAddress);
                                 AV10firstLink = cgiGet( edtavFirstlink_Internalname);
                                 AssignAttri(sPrefix, false, edtavFirstlink_Internalname, AV10firstLink);
                                 AV24secondLink = cgiGet( edtavSecondlink_Internalname);
                                 AssignAttri(sPrefix, false, edtavSecondlink_Internalname, AV24secondLink);
                                 AV45txtConfirmations = cgiGet( edtavTxtconfirmations_Internalname);
                                 AssignAttri(sPrefix, false, edtavTxtconfirmations_Internalname, AV45txtConfirmations);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlreceiveddatetime_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E14092 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ADDRESSGRID.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlreceiveddatetime_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Addressgrid.Load */
                                          E15092 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'GET KEY INFO'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlreceiveddatetime_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Get Key Info' */
                                          E16092 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          if ( ! wbErr )
                                          {
                                             Rfr0gs = false;
                                             if ( ! Rfr0gs )
                                             {
                                             }
                                             dynload_actions( ) ;
                                          }
                                       }
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                                    {
                                       STRUP090( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlreceiveddatetime_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                       }
                                    }
                                 }
                              }
                              else
                              {
                              }
                           }
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE092( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm092( ) ;
            }
         }
      }

      protected void PA092( )
      {
         if ( nDonePA == 0 )
         {
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               initialize_properties( ) ;
            }
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
            }
            init_web_controls( ) ;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = edtavRecevingaddress_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrAddressgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_272( ) ;
         while ( nGXsfl_27_idx <= nRC_GXsfl_27 )
         {
            sendrow_272( ) ;
            nGXsfl_27_idx = ((subAddressgrid_Islastpage==1)&&(nGXsfl_27_idx+1>subAddressgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_27_idx+1);
            sGXsfl_27_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_27_idx), 4, 0), 4, "0");
            SubsflControlProps_272( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( AddressgridContainer)) ;
         /* End function gxnrAddressgrid_newrow */
      }

      protected void gxgrAddressgrid_refresh( int subAddressgrid_Rows ,
                                              GeneXus.Programs.wallet.SdtWallet AV31wallet ,
                                              GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV23SDTAddressHistory ,
                                              string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         ADDRESSGRID_nCurrentRecord = 0;
         RF092( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrAddressgrid_refresh */
      }

      protected void send_integrity_hashes( )
      {
      }

      protected void clear_multi_value_controls( )
      {
         if ( context.isAjaxRequest( ) )
         {
            dynload_actions( ) ;
            before_start_formulas( ) ;
         }
      }

      protected void fix_multi_value_controls( )
      {
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF092( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavRecevingaddress_Enabled = 0;
         AssignProp(sPrefix, false, edtavRecevingaddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRecevingaddress_Enabled), 5, 0), true);
         edtavTotalbalance_Enabled = 0;
         AssignProp(sPrefix, false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
         edtavTransactionscount_Enabled = 0;
         AssignProp(sPrefix, false, edtavTransactionscount_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTransactionscount_Enabled), 5, 0), true);
         edtavCtlreceiveddatetime_Enabled = 0;
         edtavGridrecevingaddress_Enabled = 0;
         edtavCtlreceivedamount_Enabled = 0;
         edtavCtlreceivedtransactionid_Enabled = 0;
         edtavFirstlink_Enabled = 0;
         edtavCtlsentdatetime_Enabled = 0;
         edtavCtlsenttransactionid_Enabled = 0;
         edtavSecondlink_Enabled = 0;
         edtavCtlbalance_Enabled = 0;
         edtavTxtconfirmations_Enabled = 0;
      }

      protected void RF092( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            AddressgridContainer.ClearRows();
         }
         wbStart = 27;
         /* Execute user event: Refresh */
         E14092 ();
         nGXsfl_27_idx = 1;
         sGXsfl_27_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_27_idx), 4, 0), 4, "0");
         SubsflControlProps_272( ) ;
         bGXsfl_27_Refreshing = true;
         AddressgridContainer.AddObjectProperty("GridName", "Addressgrid");
         AddressgridContainer.AddObjectProperty("CmpContext", sPrefix);
         AddressgridContainer.AddObjectProperty("InMasterPage", "false");
         AddressgridContainer.AddObjectProperty("Class", "Grid");
         AddressgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         AddressgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         AddressgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressgrid_Backcolorstyle), 1, 0, ".", "")));
         AddressgridContainer.PageSize = subAddressgrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_272( ) ;
            /* Execute user event: Addressgrid.Load */
            E15092 ();
            if ( ( subAddressgrid_Islastpage == 0 ) && ( ADDRESSGRID_nCurrentRecord > 0 ) && ( ADDRESSGRID_nGridOutOfScope == 0 ) && ( nGXsfl_27_idx == 1 ) )
            {
               ADDRESSGRID_nCurrentRecord = 0;
               ADDRESSGRID_nGridOutOfScope = 1;
               subaddressgrid_firstpage( ) ;
               /* Execute user event: Addressgrid.Load */
               E15092 ();
            }
            wbEnd = 27;
            WB090( ) ;
         }
         bGXsfl_27_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes092( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWALLET", AV31wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWALLET", AV31wallet);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWALLET", GetSecureSignedToken( sPrefix, AV31wallet, context));
      }

      protected int subAddressgrid_fnc_Pagecount( )
      {
         ADDRESSGRID_nRecordCount = subAddressgrid_fnc_Recordcount( );
         if ( ((int)((ADDRESSGRID_nRecordCount) % (subAddressgrid_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(ADDRESSGRID_nRecordCount/ (decimal)(subAddressgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(ADDRESSGRID_nRecordCount/ (decimal)(subAddressgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subAddressgrid_fnc_Recordcount( )
      {
         return AV23SDTAddressHistory.Count ;
      }

      protected int subAddressgrid_fnc_Recordsperpage( )
      {
         return (int)(20*1) ;
      }

      protected int subAddressgrid_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(ADDRESSGRID_nFirstRecordOnPage/ (decimal)(subAddressgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subaddressgrid_firstpage( )
      {
         ADDRESSGRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrAddressgrid_refresh( subAddressgrid_Rows, AV31wallet, AV23SDTAddressHistory, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subaddressgrid_nextpage( )
      {
         ADDRESSGRID_nRecordCount = subAddressgrid_fnc_Recordcount( );
         if ( ( ADDRESSGRID_nRecordCount >= subAddressgrid_fnc_Recordsperpage( ) ) && ( ADDRESSGRID_nEOF == 0 ) )
         {
            ADDRESSGRID_nFirstRecordOnPage = (long)(ADDRESSGRID_nFirstRecordOnPage+subAddressgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         AddressgridContainer.AddObjectProperty("ADDRESSGRID_nFirstRecordOnPage", ADDRESSGRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrAddressgrid_refresh( subAddressgrid_Rows, AV31wallet, AV23SDTAddressHistory, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((ADDRESSGRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subaddressgrid_previouspage( )
      {
         if ( ADDRESSGRID_nFirstRecordOnPage >= subAddressgrid_fnc_Recordsperpage( ) )
         {
            ADDRESSGRID_nFirstRecordOnPage = (long)(ADDRESSGRID_nFirstRecordOnPage-subAddressgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrAddressgrid_refresh( subAddressgrid_Rows, AV31wallet, AV23SDTAddressHistory, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subaddressgrid_lastpage( )
      {
         ADDRESSGRID_nRecordCount = subAddressgrid_fnc_Recordcount( );
         if ( ADDRESSGRID_nRecordCount > subAddressgrid_fnc_Recordsperpage( ) )
         {
            if ( ((int)((ADDRESSGRID_nRecordCount) % (subAddressgrid_fnc_Recordsperpage( )))) == 0 )
            {
               ADDRESSGRID_nFirstRecordOnPage = (long)(ADDRESSGRID_nRecordCount-subAddressgrid_fnc_Recordsperpage( ));
            }
            else
            {
               ADDRESSGRID_nFirstRecordOnPage = (long)(ADDRESSGRID_nRecordCount-((int)((ADDRESSGRID_nRecordCount) % (subAddressgrid_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            ADDRESSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrAddressgrid_refresh( subAddressgrid_Rows, AV31wallet, AV23SDTAddressHistory, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subaddressgrid_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            ADDRESSGRID_nFirstRecordOnPage = (long)(subAddressgrid_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            ADDRESSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrAddressgrid_refresh( subAddressgrid_Rows, AV31wallet, AV23SDTAddressHistory, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavRecevingaddress_Enabled = 0;
         AssignProp(sPrefix, false, edtavRecevingaddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRecevingaddress_Enabled), 5, 0), true);
         edtavTotalbalance_Enabled = 0;
         AssignProp(sPrefix, false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
         edtavTransactionscount_Enabled = 0;
         AssignProp(sPrefix, false, edtavTransactionscount_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTransactionscount_Enabled), 5, 0), true);
         edtavCtlreceiveddatetime_Enabled = 0;
         edtavGridrecevingaddress_Enabled = 0;
         edtavCtlreceivedamount_Enabled = 0;
         edtavCtlreceivedtransactionid_Enabled = 0;
         edtavFirstlink_Enabled = 0;
         edtavCtlsentdatetime_Enabled = 0;
         edtavCtlsenttransactionid_Enabled = 0;
         edtavSecondlink_Enabled = 0;
         edtavCtlbalance_Enabled = 0;
         edtavTxtconfirmations_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP090( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Sdtaddresshistory"), AV23SDTAddressHistory);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vSDTADDRESSHISTORY"), AV23SDTAddressHistory);
            /* Read saved values. */
            nRC_GXsfl_27 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_27"), ".", ","), 18, MidpointRounding.ToEven));
            ADDRESSGRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"ADDRESSGRID_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            ADDRESSGRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"ADDRESSGRID_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_27 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_27"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_27_fel_idx = 0;
            while ( nGXsfl_27_fel_idx < nRC_GXsfl_27 )
            {
               nGXsfl_27_fel_idx = ((subAddressgrid_Islastpage==1)&&(nGXsfl_27_fel_idx+1>subAddressgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_27_fel_idx+1);
               sGXsfl_27_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_27_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_272( ) ;
               AV48GXV1 = (int)(nGXsfl_27_fel_idx+ADDRESSGRID_nFirstRecordOnPage);
               if ( ( AV23SDTAddressHistory.Count >= AV48GXV1 ) && ( AV48GXV1 > 0 ) )
               {
                  AV23SDTAddressHistory.CurrentItem = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1));
                  AV47gridRecevingAddress = cgiGet( edtavGridrecevingaddress_Internalname);
                  AV10firstLink = cgiGet( edtavFirstlink_Internalname);
                  AV24secondLink = cgiGet( edtavSecondlink_Internalname);
                  AV45txtConfirmations = cgiGet( edtavTxtconfirmations_Internalname);
               }
            }
            if ( nGXsfl_27_fel_idx == 0 )
            {
               nGXsfl_27_idx = 1;
               sGXsfl_27_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_27_idx), 4, 0), 4, "0");
               SubsflControlProps_272( ) ;
            }
            nGXsfl_27_fel_idx = 1;
            /* Read variables values. */
            AV32recevingAddress = cgiGet( edtavRecevingaddress_Internalname);
            AssignAttri(sPrefix, false, "AV32recevingAddress", AV32recevingAddress);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",") > 999999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vTOTALBALANCE");
               GX_FocusControl = edtavTotalbalance_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV27totalBalance = 0;
               AssignAttri(sPrefix, false, "AV27totalBalance", StringUtil.LTrimStr( AV27totalBalance, 18, 8));
            }
            else
            {
               AV27totalBalance = context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",");
               AssignAttri(sPrefix, false, "AV27totalBalance", StringUtil.LTrimStr( AV27totalBalance, 18, 8));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavTransactionscount_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavTransactionscount_Internalname), ".", ",") > Convert.ToDecimal( 9999999999L )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vTRANSACTIONSCOUNT");
               GX_FocusControl = edtavTransactionscount_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV29transactionsCount = 0;
               AssignAttri(sPrefix, false, "AV29transactionsCount", StringUtil.LTrimStr( (decimal)(AV29transactionsCount), 10, 0));
            }
            else
            {
               AV29transactionsCount = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavTransactionscount_Internalname), ".", ","), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV29transactionsCount", StringUtil.LTrimStr( (decimal)(AV29transactionsCount), 10, 0));
            }
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            /* Check if conditions changed and reset current page numbers */
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void E14092( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         GXt_SdtExtKeyInfo1 = AV9extKeyInfoRoot;
         new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo1) ;
         AV9extKeyInfoRoot = GXt_SdtExtKeyInfo1;
         GXt_SdtWallet2 = AV31wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet2) ;
         AV31wallet = GXt_SdtWallet2;
         AV27totalBalance = 0;
         AssignAttri(sPrefix, false, "AV27totalBalance", StringUtil.LTrimStr( AV27totalBalance, 18, 8));
         GXt_decimal3 = AV27totalBalance;
         new GeneXus.Programs.wallet.createunusedaddresses(context ).execute( out  AV28transactions__postInput, out  AV23SDTAddressHistory, out  GXt_decimal3) ;
         gx_BV27 = true;
         AV27totalBalance = GXt_decimal3;
         AssignAttri(sPrefix, false, "AV27totalBalance", StringUtil.LTrimStr( AV27totalBalance, 18, 8));
         /* Execute user subroutine: 'SHOW QRCODE' */
         S112 ();
         if (returnInSub) return;
         if ( ( AV27totalBalance > Convert.ToDecimal( 0 )) )
         {
            bttSend_Visible = 1;
            AssignProp(sPrefix, false, bttSend_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSend_Visible), 5, 0), true);
         }
         else
         {
            bttSend_Visible = 0;
            AssignProp(sPrefix, false, bttSend_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSend_Visible), 5, 0), true);
         }
         if ( AV31wallet.gxTpr_Walletreadbalanceonstart )
         {
            /* Execute user subroutine: 'GET BALANCE' */
            S122 ();
            if (returnInSub) return;
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV31wallet", AV31wallet);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV23SDTAddressHistory", AV23SDTAddressHistory);
      }

      private void E15092( )
      {
         /* Addressgrid_Load Routine */
         returnInSub = false;
         AV48GXV1 = 1;
         while ( AV48GXV1 <= AV23SDTAddressHistory.Count )
         {
            AV23SDTAddressHistory.CurrentItem = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1));
            if ( StringUtil.StrCmp(AV31wallet.gxTpr_Networktype, "MainNet") == 0 )
            {
               edtavFirstlink_Link = "https://blockstream.info/tx/"+StringUtil.Trim( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Receivedtransactionid);
               edtavSecondlink_Link = "https://blockstream.info/tx/"+StringUtil.Trim( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Senttransactionid);
            }
            else
            {
               edtavFirstlink_Link = "https://blockstream.info/testnet/tx/"+StringUtil.Trim( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Receivedtransactionid);
               edtavSecondlink_Link = "https://blockstream.info/testnet/tx/"+StringUtil.Trim( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Senttransactionid);
            }
            AV10firstLink = "Inspect on Chain";
            AssignAttri(sPrefix, false, edtavFirstlink_Internalname, AV10firstLink);
            edtavFirstlink_Linktarget = "_blank";
            if ( ( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Balance > Convert.ToDecimal( 0 )) )
            {
               AV24secondLink = "";
               AssignAttri(sPrefix, false, edtavSecondlink_Internalname, AV24secondLink);
            }
            else
            {
               AV24secondLink = "Inspect on Chain";
               AssignAttri(sPrefix, false, edtavSecondlink_Internalname, AV24secondLink);
               edtavSecondlink_Linktarget = "_blank";
            }
            AV29transactionsCount = AV23SDTAddressHistory.Count;
            AssignAttri(sPrefix, false, "AV29transactionsCount", StringUtil.LTrimStr( (decimal)(AV29transactionsCount), 10, 0));
            if ( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Confirmations >= 6 )
            {
               AV45txtConfirmations = "CONFIRMED";
               AssignAttri(sPrefix, false, edtavTxtconfirmations_Internalname, AV45txtConfirmations);
            }
            else if ( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Confirmations == 0 )
            {
               AV45txtConfirmations = "IN MEMPOOL";
               AssignAttri(sPrefix, false, edtavTxtconfirmations_Internalname, AV45txtConfirmations);
            }
            else
            {
               AV45txtConfirmations = StringUtil.Trim( StringUtil.Str( (decimal)(((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Confirmations), 10, 0));
               AssignAttri(sPrefix, false, edtavTxtconfirmations_Internalname, AV45txtConfirmations);
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Description)) )
            {
               AV47gridRecevingAddress = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Receivedaddress);
               AssignAttri(sPrefix, false, edtavGridrecevingaddress_Internalname, AV47gridRecevingAddress);
            }
            else
            {
               AV47gridRecevingAddress = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Description);
               AssignAttri(sPrefix, false, edtavGridrecevingaddress_Internalname, AV47gridRecevingAddress);
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 27;
            }
            if ( ( subAddressgrid_Islastpage == 1 ) || ( 20 == 0 ) || ( ( ADDRESSGRID_nCurrentRecord >= ADDRESSGRID_nFirstRecordOnPage ) && ( ADDRESSGRID_nCurrentRecord < ADDRESSGRID_nFirstRecordOnPage + subAddressgrid_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_272( ) ;
            }
            ADDRESSGRID_nEOF = (short)(((ADDRESSGRID_nCurrentRecord<ADDRESSGRID_nFirstRecordOnPage+subAddressgrid_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"ADDRESSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(ADDRESSGRID_nEOF), 1, 0, ".", "")));
            ADDRESSGRID_nCurrentRecord = (long)(ADDRESSGRID_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_27_Refreshing )
            {
               DoAjaxLoad(27, AddressgridRow);
            }
            AV48GXV1 = (int)(AV48GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E16092( )
      {
         AV48GXV1 = (int)(nGXsfl_27_idx+ADDRESSGRID_nFirstRecordOnPage);
         if ( ( AV48GXV1 > 0 ) && ( AV23SDTAddressHistory.Count >= AV48GXV1 ) )
         {
            AV23SDTAddressHistory.CurrentItem = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1));
         }
         /* 'Get Key Info' Routine */
         returnInSub = false;
         context.PopUp(formatLink("wallet.showtransactiondetail.aspx", new object[] {UrlEncode(StringUtil.RTrim(((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Receivedtransactionid)),UrlEncode(StringUtil.LTrimStr(((GeneXus.Programs.wallet.SdtSDTAddressHistory)(AV23SDTAddressHistory.CurrentItem)).gxTpr_Recivedn,10,0)),UrlEncode(StringUtil.RTrim("transactions.trn"))}, new string[] {"transactionId","n","transactionFileName"}) , new Object[] {});
      }

      protected void E12092( )
      {
         AV48GXV1 = (int)(nGXsfl_27_idx+ADDRESSGRID_nFirstRecordOnPage);
         if ( ( AV48GXV1 > 0 ) && ( AV23SDTAddressHistory.Count >= AV48GXV1 ) )
         {
            AV23SDTAddressHistory.CurrentItem = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1));
         }
         /* 'Update Balance' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GET BALANCE' */
         S122 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         if ( gx_BV27 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV23SDTAddressHistory", AV23SDTAddressHistory);
            nGXsfl_27_bak_idx = nGXsfl_27_idx;
            gxgrAddressgrid_refresh( subAddressgrid_Rows, AV31wallet, AV23SDTAddressHistory, sPrefix) ;
            nGXsfl_27_idx = nGXsfl_27_bak_idx;
            sGXsfl_27_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_27_idx), 4, 0), 4, "0");
            SubsflControlProps_272( ) ;
         }
      }

      protected void E13092( )
      {
         AV48GXV1 = (int)(nGXsfl_27_idx+ADDRESSGRID_nFirstRecordOnPage);
         if ( ( AV48GXV1 > 0 ) && ( AV23SDTAddressHistory.Count >= AV48GXV1 ) )
         {
            AV23SDTAddressHistory.CurrentItem = ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1));
         }
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV25sendCoinPopupName = "Wallet.SendCoins";
         AV34strFound = (short)(StringUtil.StringSearch( AV19PopupName, StringUtil.Lower( AV25sendCoinPopupName), 1));
         if ( AV34strFound > 0 )
         {
            /* Execute user subroutine: 'GET BALANCE' */
            S122 ();
            if (returnInSub) return;
         }
         else
         {
            AV25sendCoinPopupName = "Wallet.ShowTransactionDetail";
            AV34strFound = (short)(StringUtil.StringSearch( AV19PopupName, StringUtil.Lower( AV25sendCoinPopupName), 1));
            if ( AV34strFound > 0 )
            {
               context.DoAjaxRefreshCmp(sPrefix);
            }
         }
         /*  Sending Event outputs  */
         if ( gx_BV27 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV23SDTAddressHistory", AV23SDTAddressHistory);
            nGXsfl_27_bak_idx = nGXsfl_27_idx;
            gxgrAddressgrid_refresh( subAddressgrid_Rows, AV31wallet, AV23SDTAddressHistory, sPrefix) ;
            nGXsfl_27_idx = nGXsfl_27_bak_idx;
            sGXsfl_27_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_27_idx), 4, 0), 4, "0");
            SubsflControlProps_272( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV31wallet", AV31wallet);
      }

      protected void S122( )
      {
         /* 'GET BALANCE' Routine */
         returnInSub = false;
         AV23SDTAddressHistory.Clear();
         gx_BV27 = true;
         AV12historyWithBalance.Clear();
         new GeneXus.Programs.wallet.sethistorywithbalance(context ).execute(  AV12historyWithBalance) ;
         AV27totalBalance = 0;
         AssignAttri(sPrefix, false, "AV27totalBalance", StringUtil.LTrimStr( AV27totalBalance, 18, 8));
         AV28transactions__postInput = new SdtGetTransactions__postInput(context);
         GXt_decimal3 = AV27totalBalance;
         new GeneXus.Programs.wallet.createunusedaddresses(context ).execute( out  AV28transactions__postInput, out  AV23SDTAddressHistory, out  GXt_decimal3) ;
         gx_BV27 = true;
         AV27totalBalance = GXt_decimal3;
         AssignAttri(sPrefix, false, "AV27totalBalance", StringUtil.LTrimStr( AV27totalBalance, 18, 8));
         /* Execute user subroutine: 'SHOW QRCODE' */
         S112 ();
         if (returnInSub) return;
         GXt_char4 = AV8error;
         new GeneXus.Programs.wallet.gettransactionsfromservice(context ).execute(  "transactions.trn",  AV28transactions__postInput, out  AV37StoredTransactions, out  GXt_char4) ;
         AV8error = GXt_char4;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            GXt_decimal3 = AV27totalBalance;
            new GeneXus.Programs.wallet.loadhistoryfromtransactions(context ).execute(  AV37StoredTransactions, out  AV23SDTAddressHistory, out  GXt_decimal3) ;
            gx_BV27 = true;
            AV27totalBalance = GXt_decimal3;
            AssignAttri(sPrefix, false, "AV27totalBalance", StringUtil.LTrimStr( AV27totalBalance, 18, 8));
            if ( ( AV27totalBalance > Convert.ToDecimal( 0 )) )
            {
               bttSend_Visible = 1;
               AssignProp(sPrefix, false, bttSend_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSend_Visible), 5, 0), true);
            }
            else
            {
               bttSend_Visible = 0;
               AssignProp(sPrefix, false, bttSend_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSend_Visible), 5, 0), true);
            }
         }
         else
         {
            GX_msglist.addItem(AV8error);
         }
      }

      protected void S112( )
      {
         /* 'SHOW QRCODE' Routine */
         returnInSub = false;
         GXt_char4 = AV32recevingAddress;
         new GeneXus.Programs.wallet.pulloneaddress(context ).execute(  (short)(Math.Round(NumberUtil.Val( "0", "."), 18, MidpointRounding.ToEven)), out  GXt_char4) ;
         AV32recevingAddress = GXt_char4;
         AssignAttri(sPrefix, false, "AV32recevingAddress", AV32recevingAddress);
         GXt_char4 = AV44addresImageText;
         new GeneXus.Programs.qrcoder.generateqrcodestring(context ).execute(  "bitcoin:"+AV32recevingAddress, out  GXt_char4) ;
         AV44addresImageText = GXt_char4;
         lblTbqrcodeaddress_Caption = "<img src=\"data:image/png;base64, "+AV44addresImageText+"   \" style=\"width: 250px\" />";
         AssignProp(sPrefix, false, lblTbqrcodeaddress_Internalname, "Caption", lblTbqrcodeaddress_Caption, true);
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
      }

      public override string getresponse( string sGXDynURL )
      {
         initialize_properties( ) ;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         sDynURL = sGXDynURL;
         nGotPars = (short)(1);
         nGXWrapped = (short)(1);
         context.SetWrapped(true);
         PA092( ) ;
         WS092( ) ;
         WE092( ) ;
         cleanup();
         context.SetWrapped(false);
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      public override void componentbind( Object[] obj )
      {
         if ( IsUrlCreated( ) )
         {
            return  ;
         }
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA092( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\multyaddress", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA092( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
         }
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
      }

      public override void componentprocess( string sPPrefix ,
                                             string sPSFPrefix ,
                                             string sCompEvt )
      {
         sCompPrefix = sPPrefix;
         sSFPrefix = sPSFPrefix;
         sPrefix = sCompPrefix + sSFPrefix;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         INITWEB( ) ;
         nDraw = 0;
         PA092( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS092( ) ;
         if ( isFullAjaxMode( ) )
         {
            componentdraw();
         }
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override void componentstart( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
      }

      protected void WCStart( )
      {
         nDraw = 1;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WS092( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
      }

      public override void componentdraw( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WCParametersSet( ) ;
         WE092( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override string getstring( string sGXControl )
      {
         string sCtrlName;
         if ( StringUtil.StrCmp(StringUtil.Substring( sGXControl, 1, 1), "&") == 0 )
         {
            sCtrlName = StringUtil.Substring( sGXControl, 2, StringUtil.Len( sGXControl)-1);
         }
         else
         {
            sCtrlName = sGXControl;
         }
         return cgiGet( sPrefix+"v"+StringUtil.Upper( sCtrlName)) ;
      }

      public override void componentjscripts( )
      {
         include_jscripts( ) ;
      }

      public override void componentthemes( )
      {
         define_styles( ) ;
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202531412573840", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         CloseStyles();
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("wallet/multyaddress.js", "?202531412573840", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_272( )
      {
         edtavCtlreceiveddatetime_Internalname = sPrefix+"CTLRECEIVEDDATETIME_"+sGXsfl_27_idx;
         edtavGridrecevingaddress_Internalname = sPrefix+"vGRIDRECEVINGADDRESS_"+sGXsfl_27_idx;
         edtavCtlreceivedamount_Internalname = sPrefix+"CTLRECEIVEDAMOUNT_"+sGXsfl_27_idx;
         edtavCtlreceivedtransactionid_Internalname = sPrefix+"CTLRECEIVEDTRANSACTIONID_"+sGXsfl_27_idx;
         edtavFirstlink_Internalname = sPrefix+"vFIRSTLINK_"+sGXsfl_27_idx;
         edtavCtlsentdatetime_Internalname = sPrefix+"CTLSENTDATETIME_"+sGXsfl_27_idx;
         edtavCtlsenttransactionid_Internalname = sPrefix+"CTLSENTTRANSACTIONID_"+sGXsfl_27_idx;
         edtavSecondlink_Internalname = sPrefix+"vSECONDLINK_"+sGXsfl_27_idx;
         edtavCtlbalance_Internalname = sPrefix+"CTLBALANCE_"+sGXsfl_27_idx;
         edtavTxtconfirmations_Internalname = sPrefix+"vTXTCONFIRMATIONS_"+sGXsfl_27_idx;
      }

      protected void SubsflControlProps_fel_272( )
      {
         edtavCtlreceiveddatetime_Internalname = sPrefix+"CTLRECEIVEDDATETIME_"+sGXsfl_27_fel_idx;
         edtavGridrecevingaddress_Internalname = sPrefix+"vGRIDRECEVINGADDRESS_"+sGXsfl_27_fel_idx;
         edtavCtlreceivedamount_Internalname = sPrefix+"CTLRECEIVEDAMOUNT_"+sGXsfl_27_fel_idx;
         edtavCtlreceivedtransactionid_Internalname = sPrefix+"CTLRECEIVEDTRANSACTIONID_"+sGXsfl_27_fel_idx;
         edtavFirstlink_Internalname = sPrefix+"vFIRSTLINK_"+sGXsfl_27_fel_idx;
         edtavCtlsentdatetime_Internalname = sPrefix+"CTLSENTDATETIME_"+sGXsfl_27_fel_idx;
         edtavCtlsenttransactionid_Internalname = sPrefix+"CTLSENTTRANSACTIONID_"+sGXsfl_27_fel_idx;
         edtavSecondlink_Internalname = sPrefix+"vSECONDLINK_"+sGXsfl_27_fel_idx;
         edtavCtlbalance_Internalname = sPrefix+"CTLBALANCE_"+sGXsfl_27_fel_idx;
         edtavTxtconfirmations_Internalname = sPrefix+"vTXTCONFIRMATIONS_"+sGXsfl_27_fel_idx;
      }

      protected void sendrow_272( )
      {
         sGXsfl_27_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_27_idx), 4, 0), 4, "0");
         SubsflControlProps_272( ) ;
         WB090( ) ;
         if ( ( 20 * 1 == 0 ) || ( nGXsfl_27_idx <= subAddressgrid_fnc_Recordsperpage( ) * 1 ) )
         {
            AddressgridRow = GXWebRow.GetNew(context,AddressgridContainer);
            if ( subAddressgrid_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subAddressgrid_Backstyle = 0;
               if ( StringUtil.StrCmp(subAddressgrid_Class, "") != 0 )
               {
                  subAddressgrid_Linesclass = subAddressgrid_Class+"Odd";
               }
            }
            else if ( subAddressgrid_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subAddressgrid_Backstyle = 0;
               subAddressgrid_Backcolor = subAddressgrid_Allbackcolor;
               if ( StringUtil.StrCmp(subAddressgrid_Class, "") != 0 )
               {
                  subAddressgrid_Linesclass = subAddressgrid_Class+"Uniform";
               }
            }
            else if ( subAddressgrid_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subAddressgrid_Backstyle = 1;
               if ( StringUtil.StrCmp(subAddressgrid_Class, "") != 0 )
               {
                  subAddressgrid_Linesclass = subAddressgrid_Class+"Odd";
               }
               subAddressgrid_Backcolor = (int)(0x0);
            }
            else if ( subAddressgrid_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subAddressgrid_Backstyle = 1;
               if ( ((int)((nGXsfl_27_idx) % (2))) == 0 )
               {
                  subAddressgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subAddressgrid_Class, "") != 0 )
                  {
                     subAddressgrid_Linesclass = subAddressgrid_Class+"Even";
                  }
               }
               else
               {
                  subAddressgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subAddressgrid_Class, "") != 0 )
                  {
                     subAddressgrid_Linesclass = subAddressgrid_Class+"Odd";
                  }
               }
            }
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_27_idx+"\">") ;
            }
            /* Subfile cell */
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'" + sPrefix + "',false,'" + sGXsfl_27_idx + "',27)\"";
            ROClassString = "Attribute";
            AddressgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlreceiveddatetime_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Receiveddatetime, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Receiveddatetime, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,28);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlreceiveddatetime_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlreceiveddatetime_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)27,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'" + sPrefix + "',false,'" + sGXsfl_27_idx + "',27)\"";
            ROClassString = "Attribute";
            AddressgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavGridrecevingaddress_Internalname,StringUtil.RTrim( AV47gridRecevingAddress),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,29);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'GET KEY INFO\\'."+sGXsfl_27_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavGridrecevingaddress_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavGridrecevingaddress_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)27,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 30,'" + sPrefix + "',false,'" + sGXsfl_27_idx + "',27)\"";
            ROClassString = "Attribute";
            AddressgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlreceivedamount_Internalname,StringUtil.LTrim( StringUtil.NToC( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Receivedamount, 16, 8, ".", "")),StringUtil.LTrim( ((edtavCtlreceivedamount_Enabled!=0) ? context.localUtil.Format( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Receivedamount, "ZZZZZZ9.99999999") : context.localUtil.Format( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Receivedamount, "ZZZZZZ9.99999999"))),TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,30);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlreceivedamount_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlreceivedamount_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)16,(short)0,(short)0,(short)27,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            AddressgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlreceivedtransactionid_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Receivedtransactionid),(string)"",""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlreceivedtransactionid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlreceivedtransactionid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)64,(short)0,(short)0,(short)27,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 32,'" + sPrefix + "',false,'" + sGXsfl_27_idx + "',27)\"";
            ROClassString = "Attribute";
            AddressgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavFirstlink_Internalname,StringUtil.RTrim( AV10firstLink),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,32);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)edtavFirstlink_Link,(string)edtavFirstlink_Linktarget,(string)"",(string)"",(string)edtavFirstlink_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavFirstlink_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)60,(short)0,(short)0,(short)27,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 33,'" + sPrefix + "',false,'" + sGXsfl_27_idx + "',27)\"";
            ROClassString = "Attribute";
            AddressgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlsentdatetime_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Sentdatetime, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Sentdatetime, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,33);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlsentdatetime_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlsentdatetime_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)27,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            AddressgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlsenttransactionid_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Senttransactionid),(string)"",""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlsenttransactionid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlsenttransactionid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)64,(short)0,(short)0,(short)27,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'" + sPrefix + "',false,'" + sGXsfl_27_idx + "',27)\"";
            ROClassString = "Attribute";
            AddressgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavSecondlink_Internalname,StringUtil.RTrim( AV24secondLink),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,35);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)edtavSecondlink_Link,(string)edtavSecondlink_Linktarget,(string)"",(string)"",(string)edtavSecondlink_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavSecondlink_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)60,(short)0,(short)0,(short)27,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 36,'" + sPrefix + "',false,'" + sGXsfl_27_idx + "',27)\"";
            ROClassString = "Attribute";
            AddressgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlbalance_Internalname,StringUtil.LTrim( StringUtil.NToC( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Balance, 16, 8, ".", "")),StringUtil.LTrim( ((edtavCtlbalance_Enabled!=0) ? context.localUtil.Format( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Balance, "ZZZZZZ9.99999999") : context.localUtil.Format( ((GeneXus.Programs.wallet.SdtSDTAddressHistory)AV23SDTAddressHistory.Item(AV48GXV1)).gxTpr_Balance, "ZZZZZZ9.99999999"))),TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,36);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlbalance_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlbalance_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)16,(short)0,(short)0,(short)27,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( AddressgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'" + sPrefix + "',false,'" + sGXsfl_27_idx + "',27)\"";
            ROClassString = "Attribute";
            AddressgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavTxtconfirmations_Internalname,StringUtil.RTrim( AV45txtConfirmations),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,37);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavTxtconfirmations_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavTxtconfirmations_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)27,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes092( ) ;
            AddressgridContainer.AddRow(AddressgridRow);
            nGXsfl_27_idx = ((subAddressgrid_Islastpage==1)&&(nGXsfl_27_idx+1>subAddressgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_27_idx+1);
            sGXsfl_27_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_27_idx), 4, 0), 4, "0");
            SubsflControlProps_272( ) ;
         }
         /* End function sendrow_272 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl27( )
      {
         if ( AddressgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"AddressgridContainer"+"DivS\" data-gxgridid=\"27\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subAddressgrid_Internalname, subAddressgrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subAddressgrid_Backcolorstyle == 0 )
            {
               subAddressgrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subAddressgrid_Class) > 0 )
               {
                  subAddressgrid_Linesclass = subAddressgrid_Class+"Title";
               }
            }
            else
            {
               subAddressgrid_Titlebackstyle = 1;
               if ( subAddressgrid_Backcolorstyle == 1 )
               {
                  subAddressgrid_Titlebackcolor = subAddressgrid_Allbackcolor;
                  if ( StringUtil.Len( subAddressgrid_Class) > 0 )
                  {
                     subAddressgrid_Linesclass = subAddressgrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subAddressgrid_Class) > 0 )
                  {
                     subAddressgrid_Linesclass = subAddressgrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Received") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Received Address") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Received Amount") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "Received Transaction Id") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Sent") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "Sent Transaction Id") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Balance") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Confirmations") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            AddressgridContainer.AddObjectProperty("GridName", "Addressgrid");
         }
         else
         {
            AddressgridContainer.AddObjectProperty("GridName", "Addressgrid");
            AddressgridContainer.AddObjectProperty("Header", subAddressgrid_Header);
            AddressgridContainer.AddObjectProperty("Class", "Grid");
            AddressgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            AddressgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            AddressgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressgrid_Backcolorstyle), 1, 0, ".", "")));
            AddressgridContainer.AddObjectProperty("CmpContext", sPrefix);
            AddressgridContainer.AddObjectProperty("InMasterPage", "false");
            AddressgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlreceiveddatetime_Enabled), 5, 0, ".", "")));
            AddressgridContainer.AddColumnProperties(AddressgridColumn);
            AddressgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV47gridRecevingAddress)));
            AddressgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavGridrecevingaddress_Enabled), 5, 0, ".", "")));
            AddressgridContainer.AddColumnProperties(AddressgridColumn);
            AddressgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlreceivedamount_Enabled), 5, 0, ".", "")));
            AddressgridContainer.AddColumnProperties(AddressgridColumn);
            AddressgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlreceivedtransactionid_Enabled), 5, 0, ".", "")));
            AddressgridContainer.AddColumnProperties(AddressgridColumn);
            AddressgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV10firstLink)));
            AddressgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavFirstlink_Enabled), 5, 0, ".", "")));
            AddressgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavFirstlink_Link));
            AddressgridColumn.AddObjectProperty("Linktarget", StringUtil.RTrim( edtavFirstlink_Linktarget));
            AddressgridContainer.AddColumnProperties(AddressgridColumn);
            AddressgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlsentdatetime_Enabled), 5, 0, ".", "")));
            AddressgridContainer.AddColumnProperties(AddressgridColumn);
            AddressgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlsenttransactionid_Enabled), 5, 0, ".", "")));
            AddressgridContainer.AddColumnProperties(AddressgridColumn);
            AddressgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV24secondLink)));
            AddressgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavSecondlink_Enabled), 5, 0, ".", "")));
            AddressgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavSecondlink_Link));
            AddressgridColumn.AddObjectProperty("Linktarget", StringUtil.RTrim( edtavSecondlink_Linktarget));
            AddressgridContainer.AddColumnProperties(AddressgridColumn);
            AddressgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlbalance_Enabled), 5, 0, ".", "")));
            AddressgridContainer.AddColumnProperties(AddressgridColumn);
            AddressgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            AddressgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV45txtConfirmations)));
            AddressgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavTxtconfirmations_Enabled), 5, 0, ".", "")));
            AddressgridContainer.AddColumnProperties(AddressgridColumn);
            AddressgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressgrid_Selectedindex), 4, 0, ".", "")));
            AddressgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressgrid_Allowselection), 1, 0, ".", "")));
            AddressgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressgrid_Selectioncolor), 9, 0, ".", "")));
            AddressgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressgrid_Allowhovering), 1, 0, ".", "")));
            AddressgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressgrid_Hoveringcolor), 9, 0, ".", "")));
            AddressgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressgrid_Allowcollapsing), 1, 0, ".", "")));
            AddressgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subAddressgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         edtavRecevingaddress_Internalname = sPrefix+"vRECEVINGADDRESS";
         lblTbqrcodeaddress_Internalname = sPrefix+"TBQRCODEADDRESS";
         edtavTotalbalance_Internalname = sPrefix+"vTOTALBALANCE";
         edtavTransactionscount_Internalname = sPrefix+"vTRANSACTIONSCOUNT";
         bttSend_Internalname = sPrefix+"SEND";
         bttUpdatebalance_Internalname = sPrefix+"UPDATEBALANCE";
         edtavCtlreceiveddatetime_Internalname = sPrefix+"CTLRECEIVEDDATETIME";
         edtavGridrecevingaddress_Internalname = sPrefix+"vGRIDRECEVINGADDRESS";
         edtavCtlreceivedamount_Internalname = sPrefix+"CTLRECEIVEDAMOUNT";
         edtavCtlreceivedtransactionid_Internalname = sPrefix+"CTLRECEIVEDTRANSACTIONID";
         edtavFirstlink_Internalname = sPrefix+"vFIRSTLINK";
         edtavCtlsentdatetime_Internalname = sPrefix+"CTLSENTDATETIME";
         edtavCtlsenttransactionid_Internalname = sPrefix+"CTLSENTTRANSACTIONID";
         edtavSecondlink_Internalname = sPrefix+"vSECONDLINK";
         edtavCtlbalance_Internalname = sPrefix+"CTLBALANCE";
         edtavTxtconfirmations_Internalname = sPrefix+"vTXTCONFIRMATIONS";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subAddressgrid_Internalname = sPrefix+"ADDRESSGRID";
      }

      public override void initialize_properties( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         init_default_properties( ) ;
         subAddressgrid_Allowcollapsing = 0;
         subAddressgrid_Allowselection = 0;
         subAddressgrid_Header = "";
         edtavTxtconfirmations_Jsonclick = "";
         edtavTxtconfirmations_Enabled = 1;
         edtavCtlbalance_Jsonclick = "";
         edtavCtlbalance_Enabled = 0;
         edtavSecondlink_Jsonclick = "";
         edtavSecondlink_Linktarget = "";
         edtavSecondlink_Link = "";
         edtavSecondlink_Enabled = 1;
         edtavCtlsenttransactionid_Jsonclick = "";
         edtavCtlsenttransactionid_Enabled = 0;
         edtavCtlsentdatetime_Jsonclick = "";
         edtavCtlsentdatetime_Enabled = 0;
         edtavFirstlink_Jsonclick = "";
         edtavFirstlink_Linktarget = "";
         edtavFirstlink_Link = "";
         edtavFirstlink_Enabled = 1;
         edtavCtlreceivedtransactionid_Jsonclick = "";
         edtavCtlreceivedtransactionid_Enabled = 0;
         edtavCtlreceivedamount_Jsonclick = "";
         edtavCtlreceivedamount_Enabled = 0;
         edtavGridrecevingaddress_Jsonclick = "";
         edtavGridrecevingaddress_Enabled = 1;
         edtavCtlreceiveddatetime_Jsonclick = "";
         edtavCtlreceiveddatetime_Enabled = 0;
         subAddressgrid_Class = "Grid";
         subAddressgrid_Backcolorstyle = 0;
         bttSend_Visible = 1;
         edtavTransactionscount_Jsonclick = "";
         edtavTransactionscount_Enabled = 1;
         edtavTotalbalance_Jsonclick = "";
         edtavTotalbalance_Enabled = 1;
         lblTbqrcodeaddress_Caption = "TBQRCodeAddress";
         edtavRecevingaddress_Enabled = 1;
         edtavCtlbalance_Enabled = -1;
         edtavCtlsenttransactionid_Enabled = -1;
         edtavCtlsentdatetime_Enabled = -1;
         edtavCtlreceivedtransactionid_Enabled = -1;
         edtavCtlreceivedamount_Enabled = -1;
         edtavCtlreceiveddatetime_Enabled = -1;
         subAddressgrid_Rows = 20;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"ADDRESSGRID_nEOF"},{"av":"subAddressgrid_Rows","ctrl":"ADDRESSGRID","prop":"Rows"},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"av":"sPrefix"},{"av":"AV31wallet","fld":"vWALLET","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV31wallet","fld":"vWALLET","hsh":true},{"av":"AV27totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZZZ9.99999999"},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"ctrl":"SEND","prop":"Visible"},{"av":"AV32recevingAddress","fld":"vRECEVINGADDRESS"},{"av":"lblTbqrcodeaddress_Caption","ctrl":"TBQRCODEADDRESS","prop":"Caption"}]}""");
         setEventMetadata("ADDRESSGRID.LOAD","""{"handler":"E15092","iparms":[{"av":"AV31wallet","fld":"vWALLET","hsh":true},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27}]""");
         setEventMetadata("ADDRESSGRID.LOAD",""","oparms":[{"av":"edtavFirstlink_Link","ctrl":"vFIRSTLINK","prop":"Link"},{"av":"edtavSecondlink_Link","ctrl":"vSECONDLINK","prop":"Link"},{"av":"AV10firstLink","fld":"vFIRSTLINK"},{"av":"edtavFirstlink_Linktarget","ctrl":"vFIRSTLINK","prop":"Linktarget"},{"av":"edtavSecondlink_Linktarget","ctrl":"vSECONDLINK","prop":"Linktarget"},{"av":"AV24secondLink","fld":"vSECONDLINK"},{"av":"AV29transactionsCount","fld":"vTRANSACTIONSCOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV45txtConfirmations","fld":"vTXTCONFIRMATIONS"},{"av":"AV47gridRecevingAddress","fld":"vGRIDRECEVINGADDRESS"}]}""");
         setEventMetadata("'GET KEY INFO'","""{"handler":"E16092","iparms":[{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27}]}""");
         setEventMetadata("'UPDATE BALANCE'","""{"handler":"E12092","iparms":[{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"av":"ADDRESSGRID_nEOF"},{"av":"subAddressgrid_Rows","ctrl":"ADDRESSGRID","prop":"Rows"},{"av":"sPrefix"},{"av":"AV31wallet","fld":"vWALLET","hsh":true}]""");
         setEventMetadata("'UPDATE BALANCE'",""","oparms":[{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"av":"AV27totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZZZ9.99999999"},{"ctrl":"SEND","prop":"Visible"},{"av":"AV32recevingAddress","fld":"vRECEVINGADDRESS"},{"av":"lblTbqrcodeaddress_Caption","ctrl":"TBQRCODEADDRESS","prop":"Caption"}]}""");
         setEventMetadata("'SEND'","""{"handler":"E11091","iparms":[]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E13092","iparms":[{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"ADDRESSGRID_nEOF"},{"av":"subAddressgrid_Rows","ctrl":"ADDRESSGRID","prop":"Rows"},{"av":"AV31wallet","fld":"vWALLET","hsh":true},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"av":"sPrefix"},{"av":"AV19PopupName","fld":"vPOPUPNAME"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"av":"AV27totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZZZ9.99999999"},{"ctrl":"SEND","prop":"Visible"},{"av":"AV32recevingAddress","fld":"vRECEVINGADDRESS"},{"av":"lblTbqrcodeaddress_Caption","ctrl":"TBQRCODEADDRESS","prop":"Caption"},{"av":"AV31wallet","fld":"vWALLET","hsh":true}]}""");
         setEventMetadata("ADDRESSGRID_FIRSTPAGE","""{"handler":"subaddressgrid_firstpage","iparms":[{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"ADDRESSGRID_nEOF"},{"av":"subAddressgrid_Rows","ctrl":"ADDRESSGRID","prop":"Rows"},{"av":"AV31wallet","fld":"vWALLET","hsh":true},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"av":"sPrefix"}]""");
         setEventMetadata("ADDRESSGRID_FIRSTPAGE",""","oparms":[{"av":"AV31wallet","fld":"vWALLET","hsh":true},{"av":"AV27totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZZZ9.99999999"},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"ctrl":"SEND","prop":"Visible"},{"av":"AV32recevingAddress","fld":"vRECEVINGADDRESS"},{"av":"lblTbqrcodeaddress_Caption","ctrl":"TBQRCODEADDRESS","prop":"Caption"}]}""");
         setEventMetadata("ADDRESSGRID_PREVPAGE","""{"handler":"subaddressgrid_previouspage","iparms":[{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"ADDRESSGRID_nEOF"},{"av":"subAddressgrid_Rows","ctrl":"ADDRESSGRID","prop":"Rows"},{"av":"AV31wallet","fld":"vWALLET","hsh":true},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"av":"sPrefix"}]""");
         setEventMetadata("ADDRESSGRID_PREVPAGE",""","oparms":[{"av":"AV31wallet","fld":"vWALLET","hsh":true},{"av":"AV27totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZZZ9.99999999"},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"ctrl":"SEND","prop":"Visible"},{"av":"AV32recevingAddress","fld":"vRECEVINGADDRESS"},{"av":"lblTbqrcodeaddress_Caption","ctrl":"TBQRCODEADDRESS","prop":"Caption"}]}""");
         setEventMetadata("ADDRESSGRID_NEXTPAGE","""{"handler":"subaddressgrid_nextpage","iparms":[{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"ADDRESSGRID_nEOF"},{"av":"subAddressgrid_Rows","ctrl":"ADDRESSGRID","prop":"Rows"},{"av":"AV31wallet","fld":"vWALLET","hsh":true},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"av":"sPrefix"}]""");
         setEventMetadata("ADDRESSGRID_NEXTPAGE",""","oparms":[{"av":"AV31wallet","fld":"vWALLET","hsh":true},{"av":"AV27totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZZZ9.99999999"},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"ctrl":"SEND","prop":"Visible"},{"av":"AV32recevingAddress","fld":"vRECEVINGADDRESS"},{"av":"lblTbqrcodeaddress_Caption","ctrl":"TBQRCODEADDRESS","prop":"Caption"}]}""");
         setEventMetadata("ADDRESSGRID_LASTPAGE","""{"handler":"subaddressgrid_lastpage","iparms":[{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"ADDRESSGRID_nEOF"},{"av":"subAddressgrid_Rows","ctrl":"ADDRESSGRID","prop":"Rows"},{"av":"AV31wallet","fld":"vWALLET","hsh":true},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"av":"sPrefix"}]""");
         setEventMetadata("ADDRESSGRID_LASTPAGE",""","oparms":[{"av":"AV31wallet","fld":"vWALLET","hsh":true},{"av":"AV27totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZZZ9.99999999"},{"av":"AV23SDTAddressHistory","fld":"vSDTADDRESSHISTORY","grid":27},{"av":"nGXsfl_27_idx","ctrl":"GRID","prop":"GridCurrRow","grid":27},{"av":"ADDRESSGRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_27","ctrl":"ADDRESSGRID","prop":"GridRC","grid":27},{"ctrl":"SEND","prop":"Visible"},{"av":"AV32recevingAddress","fld":"vRECEVINGADDRESS"},{"av":"lblTbqrcodeaddress_Caption","ctrl":"TBQRCODEADDRESS","prop":"Caption"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Txtconfirmations","iparms":[]}""");
         return  ;
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      public override void initialize( )
      {
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV31wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         AV23SDTAddressHistory = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV19PopupName = "";
         GX_FocusControl = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV32recevingAddress = "";
         lblTbqrcodeaddress_Jsonclick = "";
         bttSend_Jsonclick = "";
         bttUpdatebalance_Jsonclick = "";
         AddressgridContainer = new GXWebGrid( context);
         sStyleString = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV47gridRecevingAddress = "";
         AV10firstLink = "";
         AV24secondLink = "";
         AV45txtConfirmations = "";
         AV9extKeyInfoRoot = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtExtKeyInfo1 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         GXt_SdtWallet2 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV28transactions__postInput = new SdtGetTransactions__postInput(context);
         AddressgridRow = new GXWebRow();
         AV25sendCoinPopupName = "";
         AV12historyWithBalance = new GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory>( context, "SDTAddressHistory", "distributedcryptography");
         AV8error = "";
         AV37StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV44addresImageText = "";
         GXt_char4 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subAddressgrid_Linesclass = "";
         ROClassString = "";
         AddressgridColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavRecevingaddress_Enabled = 0;
         edtavTotalbalance_Enabled = 0;
         edtavTransactionscount_Enabled = 0;
         edtavCtlreceiveddatetime_Enabled = 0;
         edtavGridrecevingaddress_Enabled = 0;
         edtavCtlreceivedamount_Enabled = 0;
         edtavCtlreceivedtransactionid_Enabled = 0;
         edtavFirstlink_Enabled = 0;
         edtavCtlsentdatetime_Enabled = 0;
         edtavCtlsenttransactionid_Enabled = 0;
         edtavSecondlink_Enabled = 0;
         edtavCtlbalance_Enabled = 0;
         edtavTxtconfirmations_Enabled = 0;
      }

      private short ADDRESSGRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subAddressgrid_Backcolorstyle ;
      private short AV34strFound ;
      private short nGXWrapped ;
      private short subAddressgrid_Backstyle ;
      private short subAddressgrid_Titlebackstyle ;
      private short subAddressgrid_Allowselection ;
      private short subAddressgrid_Allowhovering ;
      private short subAddressgrid_Allowcollapsing ;
      private short subAddressgrid_Collapsed ;
      private int nRC_GXsfl_27 ;
      private int subAddressgrid_Rows ;
      private int nGXsfl_27_idx=1 ;
      private int edtavRecevingaddress_Enabled ;
      private int edtavTotalbalance_Enabled ;
      private int edtavTransactionscount_Enabled ;
      private int edtavCtlreceiveddatetime_Enabled ;
      private int edtavGridrecevingaddress_Enabled ;
      private int edtavCtlreceivedamount_Enabled ;
      private int edtavCtlreceivedtransactionid_Enabled ;
      private int edtavFirstlink_Enabled ;
      private int edtavCtlsentdatetime_Enabled ;
      private int edtavCtlsenttransactionid_Enabled ;
      private int edtavSecondlink_Enabled ;
      private int edtavCtlbalance_Enabled ;
      private int edtavTxtconfirmations_Enabled ;
      private int bttSend_Visible ;
      private int AV48GXV1 ;
      private int subAddressgrid_Islastpage ;
      private int ADDRESSGRID_nGridOutOfScope ;
      private int nGXsfl_27_fel_idx=1 ;
      private int nGXsfl_27_bak_idx=1 ;
      private int idxLst ;
      private int subAddressgrid_Backcolor ;
      private int subAddressgrid_Allbackcolor ;
      private int subAddressgrid_Titlebackcolor ;
      private int subAddressgrid_Selectedindex ;
      private int subAddressgrid_Selectioncolor ;
      private int subAddressgrid_Hoveringcolor ;
      private long ADDRESSGRID_nFirstRecordOnPage ;
      private long AV29transactionsCount ;
      private long ADDRESSGRID_nCurrentRecord ;
      private long ADDRESSGRID_nRecordCount ;
      private decimal AV27totalBalance ;
      private decimal GXt_decimal3 ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_27_idx="0001" ;
      private string edtavRecevingaddress_Internalname ;
      private string edtavTotalbalance_Internalname ;
      private string edtavTransactionscount_Internalname ;
      private string edtavCtlreceiveddatetime_Internalname ;
      private string edtavGridrecevingaddress_Internalname ;
      private string edtavCtlreceivedamount_Internalname ;
      private string edtavCtlreceivedtransactionid_Internalname ;
      private string edtavFirstlink_Internalname ;
      private string edtavCtlsentdatetime_Internalname ;
      private string edtavCtlsenttransactionid_Internalname ;
      private string edtavSecondlink_Internalname ;
      private string edtavCtlbalance_Internalname ;
      private string edtavTxtconfirmations_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV19PopupName ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string AV32recevingAddress ;
      private string lblTbqrcodeaddress_Internalname ;
      private string lblTbqrcodeaddress_Caption ;
      private string lblTbqrcodeaddress_Jsonclick ;
      private string edtavTotalbalance_Jsonclick ;
      private string edtavTransactionscount_Jsonclick ;
      private string bttSend_Internalname ;
      private string bttSend_Jsonclick ;
      private string bttUpdatebalance_Internalname ;
      private string bttUpdatebalance_Jsonclick ;
      private string sStyleString ;
      private string subAddressgrid_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV47gridRecevingAddress ;
      private string AV10firstLink ;
      private string AV24secondLink ;
      private string AV45txtConfirmations ;
      private string sGXsfl_27_fel_idx="0001" ;
      private string edtavFirstlink_Link ;
      private string edtavSecondlink_Link ;
      private string edtavFirstlink_Linktarget ;
      private string edtavSecondlink_Linktarget ;
      private string AV25sendCoinPopupName ;
      private string AV8error ;
      private string GXt_char4 ;
      private string subAddressgrid_Class ;
      private string subAddressgrid_Linesclass ;
      private string ROClassString ;
      private string edtavCtlreceiveddatetime_Jsonclick ;
      private string edtavGridrecevingaddress_Jsonclick ;
      private string edtavCtlreceivedamount_Jsonclick ;
      private string edtavCtlreceivedtransactionid_Jsonclick ;
      private string edtavFirstlink_Jsonclick ;
      private string edtavCtlsentdatetime_Jsonclick ;
      private string edtavCtlsenttransactionid_Jsonclick ;
      private string edtavSecondlink_Jsonclick ;
      private string edtavCtlbalance_Jsonclick ;
      private string edtavTxtconfirmations_Jsonclick ;
      private string subAddressgrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_27_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool gx_refresh_fired ;
      private bool returnInSub ;
      private bool gx_BV27 ;
      private string AV44addresImageText ;
      private GXWebGrid AddressgridContainer ;
      private GXWebRow AddressgridRow ;
      private GXWebColumn AddressgridColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.SdtWallet AV31wallet ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV23SDTAddressHistory ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV9extKeyInfoRoot ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo1 ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet2 ;
      private SdtGetTransactions__postInput AV28transactions__postInput ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtSDTAddressHistory> AV12historyWithBalance ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV37StoredTransactions ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
