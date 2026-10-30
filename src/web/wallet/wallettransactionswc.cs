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
using System.Text.Json.Serialization;
namespace GeneXus.Programs.wallet {
   public class wallettransactionswc : GXWebComponent
   {
      public wallettransactionswc( )
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

      public wallettransactionswc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_groupId )
      {
         this.AV17groupId = aP0_groupId;
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
               gxfirstwebparm = GetFirstPar( "groupId");
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
                  AV17groupId = StringUtil.StrToGuid( GetPar( "groupId"));
                  AssignAttri(sPrefix, false, "AV17groupId", AV17groupId.ToString());
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(Guid)AV17groupId});
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
                  gxfirstwebparm = GetFirstPar( "groupId");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetFirstPar( "groupId");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Transactionsgrid") == 0 )
               {
                  gxnrTransactionsgrid_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Transactionsgrid") == 0 )
               {
                  gxgrTransactionsgrid_refresh_invoke( ) ;
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

      protected void gxnrTransactionsgrid_newrow_invoke( )
      {
         nRC_GXsfl_14 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_14"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_14_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_14_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_14_idx = GetPar( "sGXsfl_14_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrTransactionsgrid_newrow( ) ;
         /* End function gxnrTransactionsgrid_newrow_invoke */
      }

      protected void gxgrTransactionsgrid_refresh_invoke( )
      {
         subTransactionsgrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subTransactionsgrid_Rows"), "."), 18, MidpointRounding.ToEven));
         AV17groupId = StringUtil.StrToGuid( GetPar( "groupId"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV10transactions);
         AV20transactionFileName = GetPar( "transactionFileName");
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrTransactionsgrid_refresh( subTransactionsgrid_Rows, AV17groupId, AV10transactions, AV20transactionFileName, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrTransactionsgrid_refresh_invoke */
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
            PA3N2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtldatetime_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtldatetime_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldatetime_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavTxtkind_Enabled = 0;
               AssignProp(sPrefix, false, edtavTxtkind_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTxtkind_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavCtllabel_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtllabel_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtllabel_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavTxtamount_Enabled = 0;
               AssignProp(sPrefix, false, edtavTxtamount_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTxtamount_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavTxtfee_Enabled = 0;
               AssignProp(sPrefix, false, edtavTxtfee_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTxtfee_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavCtlbalance_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlbalance_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavTxtconfirmations_Enabled = 0;
               AssignProp(sPrefix, false, edtavTxtconfirmations_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTxtconfirmations_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavCtltransactionid_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtltransactionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtltransactionid_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               WS3N2( ) ;
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
            context.SendWebValue( "Transactions of the wallet") ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.wallettransactionswc", new object[] {UrlEncode(AV17groupId.ToString())}, new string[] {"groupId"}) +"\">") ;
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
         GxWebStd.gx_hidden_field( context, sPrefix+"vTRANSACTIONFILENAME", StringUtil.RTrim( AV20transactionFileName));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV20transactionFileName, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Transactions", AV10transactions);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Transactions", AV10transactions);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_14", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_14), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV17groupId", wcpOAV17groupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"vGROUPID", AV17groupId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vTRANSACTIONS", AV10transactions);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vTRANSACTIONS", AV10transactions);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vTRANSACTIONFILENAME", StringUtil.RTrim( AV20transactionFileName));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV20transactionFileName, "")), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vPOPUPNAME", StringUtil.RTrim( AV25PopupName));
         GxWebStd.gx_hidden_field( context, sPrefix+"TRANSACTIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(TRANSACTIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"TRANSACTIONSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(TRANSACTIONSGRID_nEOF), 1, 0, ".", "")));
      }

      protected void RenderHtmlCloseForm3N2( )
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
         return "Wallet.WalletTransactionsWC" ;
      }

      public override string GetPgmdesc( )
      {
         return "Transactions of the wallet" ;
      }

      protected void WB3N0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.wallettransactionswc");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbtransactionssummary_Internalname, lblTbtransactionssummary_Caption, "", "", lblTbtransactionssummary_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/WalletTransactionsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttReload_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(14), 2, 0)+","+"null"+");", "Reload", bttReload_Jsonclick, 5, "Reload", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'RELOAD\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/WalletTransactionsWC.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbtransactionsinfo_Internalname, "One row per transaction, newest first. Click the Type to see the transaction. Amount: + received, - paid to others + the fee. Balance = the balance after the transaction. The list follows the last balance check (Rescan Balance asks for every address again).", "", "", lblTbtransactionsinfo_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/WalletTransactionsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            TransactionsgridContainer.SetWrapped(nGXWrapped);
            StartGridControl14( ) ;
         }
         if ( wbEnd == 14 )
         {
            wbEnd = 0;
            nRC_GXsfl_14 = (int)(nGXsfl_14_idx-1);
            if ( TransactionsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               TransactionsgridContainer.AddObjectProperty("TRANSACTIONSGRID_nEOF", TRANSACTIONSGRID_nEOF);
               TransactionsgridContainer.AddObjectProperty("TRANSACTIONSGRID_nFirstRecordOnPage", TRANSACTIONSGRID_nFirstRecordOnPage);
               AV26GXV1 = nGXsfl_14_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"TransactionsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Transactionsgrid", TransactionsgridContainer, subTransactionsgrid_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"TransactionsgridContainerData", TransactionsgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"TransactionsgridContainerData"+"V", TransactionsgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"TransactionsgridContainerData"+"V"+"\" value='"+TransactionsgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 14 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( TransactionsgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  TransactionsgridContainer.AddObjectProperty("TRANSACTIONSGRID_nEOF", TRANSACTIONSGRID_nEOF);
                  TransactionsgridContainer.AddObjectProperty("TRANSACTIONSGRID_nFirstRecordOnPage", TRANSACTIONSGRID_nFirstRecordOnPage);
                  AV26GXV1 = nGXsfl_14_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"TransactionsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Transactionsgrid", TransactionsgridContainer, subTransactionsgrid_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"TransactionsgridContainerData", TransactionsgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"TransactionsgridContainerData"+"V", TransactionsgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"TransactionsgridContainerData"+"V"+"\" value='"+TransactionsgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START3N2( )
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
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
               }
            }
            Form.Meta.addItem("description", "Transactions of the wallet", 0) ;
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
               STRUP3N0( ) ;
            }
         }
      }

      protected void WS3N2( )
      {
         START3N2( ) ;
         EVT3N2( ) ;
      }

      protected void EVT3N2( )
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
                                 STRUP3N0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "'RELOAD'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3N0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Reload' */
                                    E113N2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3N0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E123N2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3N0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtldatetime_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "TRANSACTIONSGRIDPAGING") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3N0( ) ;
                              }
                              sEvt = cgiGet( sPrefix+"TRANSACTIONSGRIDPAGING");
                              if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                              {
                                 subtransactionsgrid_firstpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                              {
                                 subtransactionsgrid_previouspage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                              {
                                 subtransactionsgrid_nextpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                              {
                                 subtransactionsgrid_lastpage( ) ;
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 21), "TRANSACTIONSGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "'SHOW TRANSACTION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "'SHOW TRANSACTION'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3N0( ) ;
                              }
                              nGXsfl_14_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
                              SubsflControlProps_142( ) ;
                              AV26GXV1 = (int)(nGXsfl_14_idx+TRANSACTIONSGRID_nFirstRecordOnPage);
                              if ( ( AV10transactions.Count >= AV26GXV1 ) && ( AV26GXV1 > 0 ) )
                              {
                                 AV10transactions.CurrentItem = ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV10transactions.Item(AV26GXV1));
                                 AV21txtKind = cgiGet( edtavTxtkind_Internalname);
                                 AssignAttri(sPrefix, false, edtavTxtkind_Internalname, AV21txtKind);
                                 AV15txtAmount = cgiGet( edtavTxtamount_Internalname);
                                 AssignAttri(sPrefix, false, edtavTxtamount_Internalname, AV15txtAmount);
                                 AV12txtFee = cgiGet( edtavTxtfee_Internalname);
                                 AssignAttri(sPrefix, false, edtavTxtfee_Internalname, AV12txtFee);
                                 AV11txtConfirmations = cgiGet( edtavTxtconfirmations_Internalname);
                                 AssignAttri(sPrefix, false, edtavTxtconfirmations_Internalname, AV11txtConfirmations);
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
                                          GX_FocusControl = edtavCtldatetime_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E133N2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "TRANSACTIONSGRID.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtldatetime_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Transactionsgrid.Load */
                                          E143N2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'SHOW TRANSACTION'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtldatetime_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Show Transaction' */
                                          E153N2 ();
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
                                       STRUP3N0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtldatetime_Internalname;
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

      protected void WE3N2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm3N2( ) ;
            }
         }
      }

      protected void PA3N2( )
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
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrTransactionsgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_142( ) ;
         while ( nGXsfl_14_idx <= nRC_GXsfl_14 )
         {
            sendrow_142( ) ;
            nGXsfl_14_idx = ((subTransactionsgrid_Islastpage==1)&&(nGXsfl_14_idx+1>subTransactionsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_idx+1);
            sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
            SubsflControlProps_142( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( TransactionsgridContainer)) ;
         /* End function gxnrTransactionsgrid_newrow */
      }

      protected void gxgrTransactionsgrid_refresh( int subTransactionsgrid_Rows ,
                                                   Guid AV17groupId ,
                                                   GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem> AV10transactions ,
                                                   string AV20transactionFileName ,
                                                   string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         TRANSACTIONSGRID_nCurrentRecord = 0;
         RF3N2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrTransactionsgrid_refresh */
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
         RF3N2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtldatetime_Enabled = 0;
         edtavTxtkind_Enabled = 0;
         edtavCtllabel_Enabled = 0;
         edtavTxtamount_Enabled = 0;
         edtavTxtfee_Enabled = 0;
         edtavCtlbalance_Enabled = 0;
         edtavTxtconfirmations_Enabled = 0;
         edtavCtltransactionid_Enabled = 0;
      }

      protected void RF3N2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            TransactionsgridContainer.ClearRows();
         }
         wbStart = 14;
         /* Execute user event: Refresh */
         E133N2 ();
         nGXsfl_14_idx = 1;
         sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
         SubsflControlProps_142( ) ;
         bGXsfl_14_Refreshing = true;
         TransactionsgridContainer.AddObjectProperty("GridName", "Transactionsgrid");
         TransactionsgridContainer.AddObjectProperty("CmpContext", sPrefix);
         TransactionsgridContainer.AddObjectProperty("InMasterPage", "false");
         TransactionsgridContainer.AddObjectProperty("Class", "Grid");
         TransactionsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         TransactionsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         TransactionsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTransactionsgrid_Backcolorstyle), 1, 0, ".", "")));
         TransactionsgridContainer.PageSize = subTransactionsgrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_142( ) ;
            /* Execute user event: Transactionsgrid.Load */
            E143N2 ();
            if ( ( subTransactionsgrid_Islastpage == 0 ) && ( TRANSACTIONSGRID_nCurrentRecord > 0 ) && ( TRANSACTIONSGRID_nGridOutOfScope == 0 ) && ( nGXsfl_14_idx == 1 ) )
            {
               TRANSACTIONSGRID_nCurrentRecord = 0;
               TRANSACTIONSGRID_nGridOutOfScope = 1;
               subtransactionsgrid_firstpage( ) ;
               /* Execute user event: Transactionsgrid.Load */
               E143N2 ();
            }
            wbEnd = 14;
            WB3N0( ) ;
         }
         bGXsfl_14_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3N2( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"vTRANSACTIONFILENAME", StringUtil.RTrim( AV20transactionFileName));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV20transactionFileName, "")), context));
      }

      protected int subTransactionsgrid_fnc_Pagecount( )
      {
         TRANSACTIONSGRID_nRecordCount = subTransactionsgrid_fnc_Recordcount( );
         if ( ((long)((TRANSACTIONSGRID_nRecordCount) % (subTransactionsgrid_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(TRANSACTIONSGRID_nRecordCount/ (decimal)(subTransactionsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(TRANSACTIONSGRID_nRecordCount/ (decimal)(subTransactionsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subTransactionsgrid_fnc_Recordcount( )
      {
         return AV10transactions.Count ;
      }

      protected int subTransactionsgrid_fnc_Recordsperpage( )
      {
         return (int)(100*1) ;
      }

      protected int subTransactionsgrid_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(TRANSACTIONSGRID_nFirstRecordOnPage/ (decimal)(subTransactionsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subtransactionsgrid_firstpage( )
      {
         TRANSACTIONSGRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, sPrefix+"TRANSACTIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(TRANSACTIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrTransactionsgrid_refresh( subTransactionsgrid_Rows, AV17groupId, AV10transactions, AV20transactionFileName, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subtransactionsgrid_nextpage( )
      {
         TRANSACTIONSGRID_nRecordCount = subTransactionsgrid_fnc_Recordcount( );
         if ( ( TRANSACTIONSGRID_nRecordCount >= subTransactionsgrid_fnc_Recordsperpage( ) ) && ( TRANSACTIONSGRID_nEOF == 0 ) )
         {
            TRANSACTIONSGRID_nFirstRecordOnPage = (long)(TRANSACTIONSGRID_nFirstRecordOnPage+subTransactionsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"TRANSACTIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(TRANSACTIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         TransactionsgridContainer.AddObjectProperty("TRANSACTIONSGRID_nFirstRecordOnPage", TRANSACTIONSGRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrTransactionsgrid_refresh( subTransactionsgrid_Rows, AV17groupId, AV10transactions, AV20transactionFileName, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((TRANSACTIONSGRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subtransactionsgrid_previouspage( )
      {
         if ( TRANSACTIONSGRID_nFirstRecordOnPage >= subTransactionsgrid_fnc_Recordsperpage( ) )
         {
            TRANSACTIONSGRID_nFirstRecordOnPage = (long)(TRANSACTIONSGRID_nFirstRecordOnPage-subTransactionsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"TRANSACTIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(TRANSACTIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrTransactionsgrid_refresh( subTransactionsgrid_Rows, AV17groupId, AV10transactions, AV20transactionFileName, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subtransactionsgrid_lastpage( )
      {
         TRANSACTIONSGRID_nRecordCount = subTransactionsgrid_fnc_Recordcount( );
         if ( TRANSACTIONSGRID_nRecordCount > subTransactionsgrid_fnc_Recordsperpage( ) )
         {
            if ( ((long)((TRANSACTIONSGRID_nRecordCount) % (subTransactionsgrid_fnc_Recordsperpage( )))) == 0 )
            {
               TRANSACTIONSGRID_nFirstRecordOnPage = (long)(TRANSACTIONSGRID_nRecordCount-subTransactionsgrid_fnc_Recordsperpage( ));
            }
            else
            {
               TRANSACTIONSGRID_nFirstRecordOnPage = (long)(TRANSACTIONSGRID_nRecordCount-((long)((TRANSACTIONSGRID_nRecordCount) % (subTransactionsgrid_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            TRANSACTIONSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"TRANSACTIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(TRANSACTIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrTransactionsgrid_refresh( subTransactionsgrid_Rows, AV17groupId, AV10transactions, AV20transactionFileName, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subtransactionsgrid_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            TRANSACTIONSGRID_nFirstRecordOnPage = (long)(subTransactionsgrid_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            TRANSACTIONSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"TRANSACTIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(TRANSACTIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrTransactionsgrid_refresh( subTransactionsgrid_Rows, AV17groupId, AV10transactions, AV20transactionFileName, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtldatetime_Enabled = 0;
         edtavTxtkind_Enabled = 0;
         edtavCtllabel_Enabled = 0;
         edtavTxtamount_Enabled = 0;
         edtavTxtfee_Enabled = 0;
         edtavCtlbalance_Enabled = 0;
         edtavTxtconfirmations_Enabled = 0;
         edtavCtltransactionid_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3N0( )
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
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Transactions"), AV10transactions);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vTRANSACTIONS"), AV10transactions);
            /* Read saved values. */
            nRC_GXsfl_14 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_14"), ".", ","), 18, MidpointRounding.ToEven));
            wcpOAV17groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV17groupId"));
            TRANSACTIONSGRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"TRANSACTIONSGRID_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            TRANSACTIONSGRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"TRANSACTIONSGRID_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_14 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_14"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_14_fel_idx = 0;
            while ( nGXsfl_14_fel_idx < nRC_GXsfl_14 )
            {
               nGXsfl_14_fel_idx = ((subTransactionsgrid_Islastpage==1)&&(nGXsfl_14_fel_idx+1>subTransactionsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_fel_idx+1);
               sGXsfl_14_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_142( ) ;
               AV26GXV1 = (int)(nGXsfl_14_fel_idx+TRANSACTIONSGRID_nFirstRecordOnPage);
               if ( ( AV10transactions.Count >= AV26GXV1 ) && ( AV26GXV1 > 0 ) )
               {
                  AV10transactions.CurrentItem = ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV10transactions.Item(AV26GXV1));
                  AV21txtKind = cgiGet( edtavTxtkind_Internalname);
                  AV15txtAmount = cgiGet( edtavTxtamount_Internalname);
                  AV12txtFee = cgiGet( edtavTxtfee_Internalname);
                  AV11txtConfirmations = cgiGet( edtavTxtconfirmations_Internalname);
               }
            }
            if ( nGXsfl_14_fel_idx == 0 )
            {
               nGXsfl_14_idx = 1;
               sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
               SubsflControlProps_142( ) ;
            }
            nGXsfl_14_fel_idx = 1;
            /* Read variables values. */
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

      protected void E133N2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.getaddresscontext(context ).execute(  AV17groupId, out  AV20transactionFileName, out  AV19requestsFileName, out  AV18receivingType, out  AV16changeType) ;
         AssignAttri(sPrefix, false, "AV20transactionFileName", AV20transactionFileName);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV20transactionFileName, "")), context));
         new GeneXus.Programs.wallet.getwallettransactions(context ).execute(  AV20transactionFileName, out  AV10transactions, out  AV6confirmedBalance, out  AV8pendingBalance, out  AV7error) ;
         gx_BV14 = true;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The transactions can not be shown",(string)AV7error}, true);
         }
         AV9totalBalance = (decimal)(AV6confirmedBalance+AV8pendingBalance);
         lblTbtransactionssummary_Caption = StringUtil.Trim( StringUtil.Str( (decimal)(AV10transactions.Count), 9, 0))+" transaction(s). Balance "+StringUtil.Trim( StringUtil.Str( AV9totalBalance, 17, 8))+" BTC: confirmed "+StringUtil.Trim( StringUtil.Str( AV6confirmedBalance, 17, 8))+" BTC, pending "+StringUtil.Trim( StringUtil.Str( AV8pendingBalance, 17, 8))+" BTC.";
         AssignProp(sPrefix, false, lblTbtransactionssummary_Internalname, "Caption", lblTbtransactionssummary_Caption, true);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV10transactions", AV10transactions);
      }

      private void E143N2( )
      {
         /* Transactionsgrid_Load Routine */
         returnInSub = false;
         AV26GXV1 = 1;
         while ( AV26GXV1 <= AV10transactions.Count )
         {
            AV10transactions.CurrentItem = ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV10transactions.Item(AV26GXV1));
            AV21txtKind = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Kind);
            AssignAttri(sPrefix, false, edtavTxtkind_Internalname, AV21txtKind);
            if ( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Pending )
            {
               AV11txtConfirmations = "0";
               AssignAttri(sPrefix, false, edtavTxtconfirmations_Internalname, AV11txtConfirmations);
            }
            else
            {
               if ( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Confirmations >= 6 )
               {
                  AV11txtConfirmations = "6+";
                  AssignAttri(sPrefix, false, edtavTxtconfirmations_Internalname, AV11txtConfirmations);
               }
               else
               {
                  AV11txtConfirmations = StringUtil.Trim( StringUtil.Str( (decimal)(((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Confirmations), 10, 0));
                  AssignAttri(sPrefix, false, edtavTxtconfirmations_Internalname, AV11txtConfirmations);
               }
            }
            if ( ( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Received > Convert.ToDecimal( 0 )) )
            {
               AV15txtAmount = "+" + StringUtil.Trim( StringUtil.Str( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Received, 17, 8));
               AssignAttri(sPrefix, false, edtavTxtamount_Internalname, AV15txtAmount);
            }
            else
            {
               AV15txtAmount = "-" + StringUtil.Trim( StringUtil.Str( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Sent, 17, 8));
               AssignAttri(sPrefix, false, edtavTxtamount_Internalname, AV15txtAmount);
            }
            AV12txtFee = "";
            AssignAttri(sPrefix, false, edtavTxtfee_Internalname, AV12txtFee);
            if ( ( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Fee > Convert.ToDecimal( 0 )) )
            {
               AV12txtFee = StringUtil.Trim( StringUtil.Str( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Fee, 17, 8));
               AssignAttri(sPrefix, false, edtavTxtfee_Internalname, AV12txtFee);
               if ( ( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Feerate > Convert.ToDecimal( 0 )) )
               {
                  AV12txtFee += " (" + StringUtil.Trim( StringUtil.Str( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Feerate, 10, 2)) + " sat/vB)";
                  AssignAttri(sPrefix, false, edtavTxtfee_Internalname, AV12txtFee);
               }
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 14;
            }
            if ( ( subTransactionsgrid_Islastpage == 1 ) || ( 100 == 0 ) || ( ( TRANSACTIONSGRID_nCurrentRecord >= TRANSACTIONSGRID_nFirstRecordOnPage ) && ( TRANSACTIONSGRID_nCurrentRecord < TRANSACTIONSGRID_nFirstRecordOnPage + subTransactionsgrid_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_142( ) ;
            }
            TRANSACTIONSGRID_nEOF = (short)(((subTransactionsgrid_Rows==0)||(TRANSACTIONSGRID_nCurrentRecord<TRANSACTIONSGRID_nFirstRecordOnPage+subTransactionsgrid_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"TRANSACTIONSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(TRANSACTIONSGRID_nEOF), 1, 0, ".", "")));
            TRANSACTIONSGRID_nCurrentRecord = (long)(TRANSACTIONSGRID_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_14_Refreshing )
            {
               DoAjaxLoad(14, TransactionsgridRow);
            }
            AV26GXV1 = (int)(AV26GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E153N2( )
      {
         AV26GXV1 = (int)(nGXsfl_14_idx+TRANSACTIONSGRID_nFirstRecordOnPage);
         if ( ( AV26GXV1 > 0 ) && ( AV10transactions.Count >= AV26GXV1 ) )
         {
            AV10transactions.CurrentItem = ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV10transactions.Item(AV26GXV1));
         }
         /* 'Show Transaction' Routine */
         returnInSub = false;
         context.PopUp(formatLink("wallet.showwallettransaction", new object[] {UrlEncode(StringUtil.RTrim(((GeneXus.Programs.wallet.SdtWalletTransactionItem)(AV10transactions.CurrentItem)).gxTpr_Transactionid)),UrlEncode(StringUtil.RTrim(AV20transactionFileName))}, new string[] {"transactionId","transactionFileName"}) , new Object[] {});
      }

      protected void E113N2( )
      {
         /* 'Reload' Routine */
         returnInSub = false;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         if ( gx_BV14 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV10transactions", AV10transactions);
            nGXsfl_14_bak_idx = nGXsfl_14_idx;
            gxgrTransactionsgrid_refresh( subTransactionsgrid_Rows, AV17groupId, AV10transactions, AV20transactionFileName, sPrefix) ;
            nGXsfl_14_idx = nGXsfl_14_bak_idx;
            sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
            SubsflControlProps_142( ) ;
         }
      }

      protected void E123N2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         if ( gx_BV14 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV10transactions", AV10transactions);
            nGXsfl_14_bak_idx = nGXsfl_14_idx;
            gxgrTransactionsgrid_refresh( subTransactionsgrid_Rows, AV17groupId, AV10transactions, AV20transactionFileName, sPrefix) ;
            nGXsfl_14_idx = nGXsfl_14_bak_idx;
            sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
            SubsflControlProps_142( ) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV17groupId = (Guid)getParm(obj,0);
         AssignAttri(sPrefix, false, "AV17groupId", AV17groupId.ToString());
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
         PA3N2( ) ;
         WS3N2( ) ;
         WE3N2( ) ;
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
         sCtrlAV17groupId = (string)((string)getParm(obj,0));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA3N2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\wallettransactionswc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA3N2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV17groupId = (Guid)getParm(obj,2);
            AssignAttri(sPrefix, false, "AV17groupId", AV17groupId.ToString());
         }
         wcpOAV17groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV17groupId"));
         if ( ! GetJustCreated( ) && ( ( AV17groupId != wcpOAV17groupId ) ) )
         {
            setjustcreated();
         }
         wcpOAV17groupId = AV17groupId;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV17groupId = cgiGet( sPrefix+"AV17groupId_CTRL");
         if ( StringUtil.Len( sCtrlAV17groupId) > 0 )
         {
            AV17groupId = StringUtil.StrToGuid( cgiGet( sCtrlAV17groupId));
            AssignAttri(sPrefix, false, "AV17groupId", AV17groupId.ToString());
         }
         else
         {
            AV17groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"AV17groupId_PARM"));
         }
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
         PA3N2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS3N2( ) ;
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
         WS3N2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV17groupId_PARM", AV17groupId.ToString());
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV17groupId)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV17groupId_CTRL", StringUtil.RTrim( sCtrlAV17groupId));
         }
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
         WE3N2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2026103013253229", true, true, false);
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
         context.AddJavascriptSource("wallet/wallettransactionswc.js", "?2026103013253229", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_142( )
      {
         edtavCtldatetime_Internalname = sPrefix+"CTLDATETIME_"+sGXsfl_14_idx;
         edtavTxtkind_Internalname = sPrefix+"vTXTKIND_"+sGXsfl_14_idx;
         edtavCtllabel_Internalname = sPrefix+"CTLLABEL_"+sGXsfl_14_idx;
         edtavTxtamount_Internalname = sPrefix+"vTXTAMOUNT_"+sGXsfl_14_idx;
         edtavTxtfee_Internalname = sPrefix+"vTXTFEE_"+sGXsfl_14_idx;
         edtavCtlbalance_Internalname = sPrefix+"CTLBALANCE_"+sGXsfl_14_idx;
         edtavTxtconfirmations_Internalname = sPrefix+"vTXTCONFIRMATIONS_"+sGXsfl_14_idx;
         edtavCtltransactionid_Internalname = sPrefix+"CTLTRANSACTIONID_"+sGXsfl_14_idx;
      }

      protected void SubsflControlProps_fel_142( )
      {
         edtavCtldatetime_Internalname = sPrefix+"CTLDATETIME_"+sGXsfl_14_fel_idx;
         edtavTxtkind_Internalname = sPrefix+"vTXTKIND_"+sGXsfl_14_fel_idx;
         edtavCtllabel_Internalname = sPrefix+"CTLLABEL_"+sGXsfl_14_fel_idx;
         edtavTxtamount_Internalname = sPrefix+"vTXTAMOUNT_"+sGXsfl_14_fel_idx;
         edtavTxtfee_Internalname = sPrefix+"vTXTFEE_"+sGXsfl_14_fel_idx;
         edtavCtlbalance_Internalname = sPrefix+"CTLBALANCE_"+sGXsfl_14_fel_idx;
         edtavTxtconfirmations_Internalname = sPrefix+"vTXTCONFIRMATIONS_"+sGXsfl_14_fel_idx;
         edtavCtltransactionid_Internalname = sPrefix+"CTLTRANSACTIONID_"+sGXsfl_14_fel_idx;
      }

      protected void sendrow_142( )
      {
         sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
         SubsflControlProps_142( ) ;
         WB3N0( ) ;
         if ( ( 100 * 1 == 0 ) || ( nGXsfl_14_idx <= subTransactionsgrid_fnc_Recordsperpage( ) * 1 ) )
         {
            TransactionsgridRow = GXWebRow.GetNew(context,TransactionsgridContainer);
            if ( subTransactionsgrid_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subTransactionsgrid_Backstyle = 0;
               if ( StringUtil.StrCmp(subTransactionsgrid_Class, "") != 0 )
               {
                  subTransactionsgrid_Linesclass = subTransactionsgrid_Class+"Odd";
               }
            }
            else if ( subTransactionsgrid_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subTransactionsgrid_Backstyle = 0;
               subTransactionsgrid_Backcolor = subTransactionsgrid_Allbackcolor;
               if ( StringUtil.StrCmp(subTransactionsgrid_Class, "") != 0 )
               {
                  subTransactionsgrid_Linesclass = subTransactionsgrid_Class+"Uniform";
               }
            }
            else if ( subTransactionsgrid_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subTransactionsgrid_Backstyle = 1;
               if ( StringUtil.StrCmp(subTransactionsgrid_Class, "") != 0 )
               {
                  subTransactionsgrid_Linesclass = subTransactionsgrid_Class+"Odd";
               }
               subTransactionsgrid_Backcolor = (int)(0x0);
            }
            else if ( subTransactionsgrid_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subTransactionsgrid_Backstyle = 1;
               if ( ((int)((nGXsfl_14_idx) % (2))) == 0 )
               {
                  subTransactionsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subTransactionsgrid_Class, "") != 0 )
                  {
                     subTransactionsgrid_Linesclass = subTransactionsgrid_Class+"Even";
                  }
               }
               else
               {
                  subTransactionsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subTransactionsgrid_Class, "") != 0 )
                  {
                     subTransactionsgrid_Linesclass = subTransactionsgrid_Class+"Odd";
                  }
               }
            }
            if ( TransactionsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_14_idx+"\">") ;
            }
            /* Subfile cell */
            if ( TransactionsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'" + sPrefix + "',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            TransactionsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtldatetime_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV10transactions.Item(AV26GXV1)).gxTpr_Datetime, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV10transactions.Item(AV26GXV1)).gxTpr_Datetime, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onblur(this,15);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtldatetime_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtldatetime_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( TransactionsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'" + sPrefix + "',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            TransactionsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavTxtkind_Internalname,StringUtil.RTrim( AV21txtKind),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,16);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SHOW TRANSACTION\\'."+sGXsfl_14_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavTxtkind_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavTxtkind_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)40,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( TransactionsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'" + sPrefix + "',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            TransactionsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtllabel_Internalname,((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV10transactions.Item(AV26GXV1)).gxTpr_Label,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,17);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtllabel_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtllabel_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( TransactionsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'" + sPrefix + "',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            TransactionsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavTxtamount_Internalname,StringUtil.RTrim( AV15txtAmount),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavTxtamount_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavTxtamount_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)25,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( TransactionsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'" + sPrefix + "',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            TransactionsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavTxtfee_Internalname,StringUtil.RTrim( AV12txtFee),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,19);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavTxtfee_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavTxtfee_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)40,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( TransactionsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'" + sPrefix + "',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            TransactionsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlbalance_Internalname,StringUtil.LTrim( StringUtil.NToC( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV10transactions.Item(AV26GXV1)).gxTpr_Balance, 16, 8, ".", "")),StringUtil.LTrim( ((edtavCtlbalance_Enabled!=0) ? context.localUtil.Format( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV10transactions.Item(AV26GXV1)).gxTpr_Balance, "ZZZZZZ9.99999999") : context.localUtil.Format( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV10transactions.Item(AV26GXV1)).gxTpr_Balance, "ZZZZZZ9.99999999"))),TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,20);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlbalance_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlbalance_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)16,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( TransactionsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'" + sPrefix + "',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            TransactionsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavTxtconfirmations_Internalname,StringUtil.RTrim( AV11txtConfirmations),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,21);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavTxtconfirmations_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavTxtconfirmations_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( TransactionsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            TransactionsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtltransactionid_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtWalletTransactionItem)AV10transactions.Item(AV26GXV1)).gxTpr_Transactionid),(string)"",""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtltransactionid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtltransactionid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)64,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes3N2( ) ;
            TransactionsgridContainer.AddRow(TransactionsgridRow);
            nGXsfl_14_idx = ((subTransactionsgrid_Islastpage==1)&&(nGXsfl_14_idx+1>subTransactionsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_idx+1);
            sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
            SubsflControlProps_142( ) ;
         }
         /* End function sendrow_142 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl14( )
      {
         if ( TransactionsgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"TransactionsgridContainer"+"DivS\" data-gxgridid=\"14\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subTransactionsgrid_Internalname, subTransactionsgrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subTransactionsgrid_Backcolorstyle == 0 )
            {
               subTransactionsgrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subTransactionsgrid_Class) > 0 )
               {
                  subTransactionsgrid_Linesclass = subTransactionsgrid_Class+"Title";
               }
            }
            else
            {
               subTransactionsgrid_Titlebackstyle = 1;
               if ( subTransactionsgrid_Backcolorstyle == 1 )
               {
                  subTransactionsgrid_Titlebackcolor = subTransactionsgrid_Allbackcolor;
                  if ( StringUtil.Len( subTransactionsgrid_Class) > 0 )
                  {
                     subTransactionsgrid_Linesclass = subTransactionsgrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subTransactionsgrid_Class) > 0 )
                  {
                     subTransactionsgrid_Linesclass = subTransactionsgrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Date") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Type") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Label") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Amount") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Fee (rate)") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Balance") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Conf.") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "transaction Id") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            TransactionsgridContainer.AddObjectProperty("GridName", "Transactionsgrid");
         }
         else
         {
            TransactionsgridContainer.AddObjectProperty("GridName", "Transactionsgrid");
            TransactionsgridContainer.AddObjectProperty("Header", subTransactionsgrid_Header);
            TransactionsgridContainer.AddObjectProperty("Class", "Grid");
            TransactionsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            TransactionsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            TransactionsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTransactionsgrid_Backcolorstyle), 1, 0, ".", "")));
            TransactionsgridContainer.AddObjectProperty("CmpContext", sPrefix);
            TransactionsgridContainer.AddObjectProperty("InMasterPage", "false");
            TransactionsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TransactionsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtldatetime_Enabled), 5, 0, ".", "")));
            TransactionsgridContainer.AddColumnProperties(TransactionsgridColumn);
            TransactionsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TransactionsgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV21txtKind)));
            TransactionsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavTxtkind_Enabled), 5, 0, ".", "")));
            TransactionsgridContainer.AddColumnProperties(TransactionsgridColumn);
            TransactionsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TransactionsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtllabel_Enabled), 5, 0, ".", "")));
            TransactionsgridContainer.AddColumnProperties(TransactionsgridColumn);
            TransactionsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TransactionsgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV15txtAmount)));
            TransactionsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavTxtamount_Enabled), 5, 0, ".", "")));
            TransactionsgridContainer.AddColumnProperties(TransactionsgridColumn);
            TransactionsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TransactionsgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV12txtFee)));
            TransactionsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavTxtfee_Enabled), 5, 0, ".", "")));
            TransactionsgridContainer.AddColumnProperties(TransactionsgridColumn);
            TransactionsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TransactionsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlbalance_Enabled), 5, 0, ".", "")));
            TransactionsgridContainer.AddColumnProperties(TransactionsgridColumn);
            TransactionsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TransactionsgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV11txtConfirmations)));
            TransactionsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavTxtconfirmations_Enabled), 5, 0, ".", "")));
            TransactionsgridContainer.AddColumnProperties(TransactionsgridColumn);
            TransactionsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            TransactionsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtltransactionid_Enabled), 5, 0, ".", "")));
            TransactionsgridContainer.AddColumnProperties(TransactionsgridColumn);
            TransactionsgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTransactionsgrid_Selectedindex), 4, 0, ".", "")));
            TransactionsgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTransactionsgrid_Allowselection), 1, 0, ".", "")));
            TransactionsgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTransactionsgrid_Selectioncolor), 9, 0, ".", "")));
            TransactionsgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTransactionsgrid_Allowhovering), 1, 0, ".", "")));
            TransactionsgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTransactionsgrid_Hoveringcolor), 9, 0, ".", "")));
            TransactionsgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTransactionsgrid_Allowcollapsing), 1, 0, ".", "")));
            TransactionsgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subTransactionsgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         lblTbtransactionssummary_Internalname = sPrefix+"TBTRANSACTIONSSUMMARY";
         bttReload_Internalname = sPrefix+"RELOAD";
         lblTbtransactionsinfo_Internalname = sPrefix+"TBTRANSACTIONSINFO";
         edtavCtldatetime_Internalname = sPrefix+"CTLDATETIME";
         edtavTxtkind_Internalname = sPrefix+"vTXTKIND";
         edtavCtllabel_Internalname = sPrefix+"CTLLABEL";
         edtavTxtamount_Internalname = sPrefix+"vTXTAMOUNT";
         edtavTxtfee_Internalname = sPrefix+"vTXTFEE";
         edtavCtlbalance_Internalname = sPrefix+"CTLBALANCE";
         edtavTxtconfirmations_Internalname = sPrefix+"vTXTCONFIRMATIONS";
         edtavCtltransactionid_Internalname = sPrefix+"CTLTRANSACTIONID";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subTransactionsgrid_Internalname = sPrefix+"TRANSACTIONSGRID";
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
         subTransactionsgrid_Allowcollapsing = 0;
         subTransactionsgrid_Allowselection = 0;
         subTransactionsgrid_Header = "";
         edtavCtltransactionid_Jsonclick = "";
         edtavCtltransactionid_Enabled = 0;
         edtavTxtconfirmations_Jsonclick = "";
         edtavTxtconfirmations_Enabled = 1;
         edtavCtlbalance_Jsonclick = "";
         edtavCtlbalance_Enabled = 0;
         edtavTxtfee_Jsonclick = "";
         edtavTxtfee_Enabled = 1;
         edtavTxtamount_Jsonclick = "";
         edtavTxtamount_Enabled = 1;
         edtavCtllabel_Jsonclick = "";
         edtavCtllabel_Enabled = 0;
         edtavTxtkind_Jsonclick = "";
         edtavTxtkind_Enabled = 1;
         edtavCtldatetime_Jsonclick = "";
         edtavCtldatetime_Enabled = 0;
         subTransactionsgrid_Class = "Grid";
         subTransactionsgrid_Backcolorstyle = 0;
         lblTbtransactionssummary_Caption = "Summary";
         edtavCtltransactionid_Enabled = -1;
         edtavCtlbalance_Enabled = -1;
         edtavCtllabel_Enabled = -1;
         edtavCtldatetime_Enabled = -1;
         subTransactionsgrid_Rows = 100;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"TRANSACTIONSGRID_nEOF","type":"int"},{"av":"subTransactionsgrid_Rows","ctrl":"TRANSACTIONSGRID","prop":"Rows"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"},{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"lblTbtransactionssummary_Caption","ctrl":"TBTRANSACTIONSSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("TRANSACTIONSGRID.LOAD","""{"handler":"E143N2","iparms":[{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"}]""");
         setEventMetadata("TRANSACTIONSGRID.LOAD",""","oparms":[{"av":"AV21txtKind","fld":"vTXTKIND","type":"char"},{"av":"AV11txtConfirmations","fld":"vTXTCONFIRMATIONS","type":"char"},{"av":"AV15txtAmount","fld":"vTXTAMOUNT","type":"char"},{"av":"AV12txtFee","fld":"vTXTFEE","type":"char"}]}""");
         setEventMetadata("'SHOW TRANSACTION'","""{"handler":"E153N2","iparms":[{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"}]}""");
         setEventMetadata("'RELOAD'","""{"handler":"E113N2","iparms":[{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"TRANSACTIONSGRID_nEOF","type":"int"},{"av":"subTransactionsgrid_Rows","ctrl":"TRANSACTIONSGRID","prop":"Rows"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'RELOAD'",""","oparms":[{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"lblTbtransactionssummary_Caption","ctrl":"TBTRANSACTIONSSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E123N2","iparms":[{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"TRANSACTIONSGRID_nEOF","type":"int"},{"av":"subTransactionsgrid_Rows","ctrl":"TRANSACTIONSGRID","prop":"Rows"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"sPrefix","type":"char"},{"av":"AV25PopupName","fld":"vPOPUPNAME","type":"char"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"lblTbtransactionssummary_Caption","ctrl":"TBTRANSACTIONSSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("TRANSACTIONSGRID_FIRSTPAGE","""{"handler":"subtransactionsgrid_firstpage","iparms":[{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"TRANSACTIONSGRID_nEOF","type":"int"},{"av":"subTransactionsgrid_Rows","ctrl":"TRANSACTIONSGRID","prop":"Rows"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"sPrefix","type":"char"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("TRANSACTIONSGRID_FIRSTPAGE",""","oparms":[{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"lblTbtransactionssummary_Caption","ctrl":"TBTRANSACTIONSSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("TRANSACTIONSGRID_PREVPAGE","""{"handler":"subtransactionsgrid_previouspage","iparms":[{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"TRANSACTIONSGRID_nEOF","type":"int"},{"av":"subTransactionsgrid_Rows","ctrl":"TRANSACTIONSGRID","prop":"Rows"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"sPrefix","type":"char"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("TRANSACTIONSGRID_PREVPAGE",""","oparms":[{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"lblTbtransactionssummary_Caption","ctrl":"TBTRANSACTIONSSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("TRANSACTIONSGRID_NEXTPAGE","""{"handler":"subtransactionsgrid_nextpage","iparms":[{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"TRANSACTIONSGRID_nEOF","type":"int"},{"av":"subTransactionsgrid_Rows","ctrl":"TRANSACTIONSGRID","prop":"Rows"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"sPrefix","type":"char"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("TRANSACTIONSGRID_NEXTPAGE",""","oparms":[{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"lblTbtransactionssummary_Caption","ctrl":"TBTRANSACTIONSSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("TRANSACTIONSGRID_LASTPAGE","""{"handler":"subtransactionsgrid_lastpage","iparms":[{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"TRANSACTIONSGRID_nEOF","type":"int"},{"av":"subTransactionsgrid_Rows","ctrl":"TRANSACTIONSGRID","prop":"Rows"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"sPrefix","type":"char"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("TRANSACTIONSGRID_LASTPAGE",""","oparms":[{"av":"AV20transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV10transactions","fld":"vTRANSACTIONS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"TRANSACTIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"TRANSACTIONSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"lblTbtransactionssummary_Caption","ctrl":"TBTRANSACTIONSSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv5","iparms":[]}""");
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
         wcpOAV17groupId = Guid.Empty;
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV10transactions = new GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem>( context, "WalletTransactionItem", "distributedcryptography");
         AV20transactionFileName = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV25PopupName = "";
         GX_FocusControl = "";
         lblTbtransactionssummary_Jsonclick = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttReload_Jsonclick = "";
         lblTbtransactionsinfo_Jsonclick = "";
         TransactionsgridContainer = new GXWebGrid( context);
         sStyleString = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV21txtKind = "";
         AV15txtAmount = "";
         AV12txtFee = "";
         AV11txtConfirmations = "";
         AV19requestsFileName = "";
         AV7error = "";
         TransactionsgridRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV17groupId = "";
         subTransactionsgrid_Linesclass = "";
         ROClassString = "";
         TransactionsgridColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtldatetime_Enabled = 0;
         edtavTxtkind_Enabled = 0;
         edtavCtllabel_Enabled = 0;
         edtavTxtamount_Enabled = 0;
         edtavTxtfee_Enabled = 0;
         edtavCtlbalance_Enabled = 0;
         edtavTxtconfirmations_Enabled = 0;
         edtavCtltransactionid_Enabled = 0;
      }

      private short TRANSACTIONSGRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subTransactionsgrid_Backcolorstyle ;
      private short AV18receivingType ;
      private short AV16changeType ;
      private short nGXWrapped ;
      private short subTransactionsgrid_Backstyle ;
      private short subTransactionsgrid_Titlebackstyle ;
      private short subTransactionsgrid_Allowselection ;
      private short subTransactionsgrid_Allowhovering ;
      private short subTransactionsgrid_Allowcollapsing ;
      private short subTransactionsgrid_Collapsed ;
      private int nRC_GXsfl_14 ;
      private int subTransactionsgrid_Rows ;
      private int nGXsfl_14_idx=1 ;
      private int edtavCtldatetime_Enabled ;
      private int edtavTxtkind_Enabled ;
      private int edtavCtllabel_Enabled ;
      private int edtavTxtamount_Enabled ;
      private int edtavTxtfee_Enabled ;
      private int edtavCtlbalance_Enabled ;
      private int edtavTxtconfirmations_Enabled ;
      private int edtavCtltransactionid_Enabled ;
      private int AV26GXV1 ;
      private int subTransactionsgrid_Islastpage ;
      private int TRANSACTIONSGRID_nGridOutOfScope ;
      private int nGXsfl_14_fel_idx=1 ;
      private int nGXsfl_14_bak_idx=1 ;
      private int idxLst ;
      private int subTransactionsgrid_Backcolor ;
      private int subTransactionsgrid_Allbackcolor ;
      private int subTransactionsgrid_Titlebackcolor ;
      private int subTransactionsgrid_Selectedindex ;
      private int subTransactionsgrid_Selectioncolor ;
      private int subTransactionsgrid_Hoveringcolor ;
      private long TRANSACTIONSGRID_nFirstRecordOnPage ;
      private long TRANSACTIONSGRID_nCurrentRecord ;
      private long TRANSACTIONSGRID_nRecordCount ;
      private decimal AV6confirmedBalance ;
      private decimal AV8pendingBalance ;
      private decimal AV9totalBalance ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_14_idx="0001" ;
      private string AV20transactionFileName ;
      private string edtavCtldatetime_Internalname ;
      private string edtavTxtkind_Internalname ;
      private string edtavCtllabel_Internalname ;
      private string edtavTxtamount_Internalname ;
      private string edtavTxtfee_Internalname ;
      private string edtavCtlbalance_Internalname ;
      private string edtavTxtconfirmations_Internalname ;
      private string edtavCtltransactionid_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV25PopupName ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string lblTbtransactionssummary_Internalname ;
      private string lblTbtransactionssummary_Caption ;
      private string lblTbtransactionssummary_Jsonclick ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttReload_Internalname ;
      private string bttReload_Jsonclick ;
      private string lblTbtransactionsinfo_Internalname ;
      private string lblTbtransactionsinfo_Jsonclick ;
      private string sStyleString ;
      private string subTransactionsgrid_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV21txtKind ;
      private string AV15txtAmount ;
      private string AV12txtFee ;
      private string AV11txtConfirmations ;
      private string sGXsfl_14_fel_idx="0001" ;
      private string AV19requestsFileName ;
      private string AV7error ;
      private string sCtrlAV17groupId ;
      private string subTransactionsgrid_Class ;
      private string subTransactionsgrid_Linesclass ;
      private string ROClassString ;
      private string edtavCtldatetime_Jsonclick ;
      private string edtavTxtkind_Jsonclick ;
      private string edtavCtllabel_Jsonclick ;
      private string edtavTxtamount_Jsonclick ;
      private string edtavTxtfee_Jsonclick ;
      private string edtavCtlbalance_Jsonclick ;
      private string edtavTxtconfirmations_Jsonclick ;
      private string edtavCtltransactionid_Jsonclick ;
      private string subTransactionsgrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_14_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool gx_refresh_fired ;
      private bool returnInSub ;
      private bool gx_BV14 ;
      private Guid AV17groupId ;
      private Guid wcpOAV17groupId ;
      private GXWebGrid TransactionsgridContainer ;
      private GXWebRow TransactionsgridRow ;
      private GXWebColumn TransactionsgridColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWalletTransactionItem> AV10transactions ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
