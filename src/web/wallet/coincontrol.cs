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
   public class coincontrol : GXHttpHandler
   {
      public coincontrol( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public coincontrol( IGxContext context )
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

      protected override void createObjects( )
      {
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Coinsgrid") == 0 )
            {
               gxnrCoinsgrid_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Coinsgrid") == 0 )
            {
               gxgrCoinsgrid_refresh_invoke( ) ;
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
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      protected void gxnrCoinsgrid_newrow_invoke( )
      {
         nRC_GXsfl_14 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_14"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_14_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_14_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_14_idx = GetPar( "sGXsfl_14_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrCoinsgrid_newrow( ) ;
         /* End function gxnrCoinsgrid_newrow_invoke */
      }

      protected void gxgrCoinsgrid_refresh_invoke( )
      {
         subCoinsgrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subCoinsgrid_Rows"), "."), 18, MidpointRounding.ToEven));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV6coins);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrCoinsgrid_refresh( subCoinsgrid_Rows, AV6coins) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrCoinsgrid_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            ValidateSpaRequest();
            PA3K2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavTotalbalance_Enabled = 0;
               AssignProp("", false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
               edtavUsecoin_Enabled = 0;
               AssignProp("", false, edtavUsecoin_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUsecoin_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavCtlamount_Enabled = 0;
               AssignProp("", false, edtavCtlamount_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlamount_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavCoinlabel_Enabled = 0;
               AssignProp("", false, edtavCoinlabel_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCoinlabel_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavTxtconfirmations_Enabled = 0;
               AssignProp("", false, edtavTxtconfirmations_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTxtconfirmations_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavCtlreceiveddatetime_Enabled = 0;
               AssignProp("", false, edtavCtlreceiveddatetime_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlreceiveddatetime_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               edtavCtloutpoint_Enabled = 0;
               AssignProp("", false, edtavCtloutpoint_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtloutpoint_Enabled), 5, 0), !bGXsfl_14_Refreshing);
               WS3K2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  WE3K2( ) ;
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
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( "Coin Control: choose the coins to spend") ;
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
         CloseStyles();
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.coincontrol") +"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal Form", true);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Coins", AV6coins);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Coins", AV6coins);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_14", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_14), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vCOINS", AV6coins);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vCOINS", AV6coins);
         }
         GxWebStd.gx_hidden_field( context, "vSELECTEDCOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV12selectedCount), 10, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "COINSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(COINSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "COINSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(COINSGRID_nEOF), 1, 0, ".", "")));
      }

      protected void RenderHtmlCloseForm3K2( )
      {
         SendCloseFormHiddens( ) ;
         GxWebStd.gx_hidden_field( context, "GX_FocusControl", GX_FocusControl);
         SendAjaxEncryptionKey();
         SendSecurityToken((string)(sPrefix));
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

      public override string GetPgmname( )
      {
         return "Wallet.CoinControl" ;
      }

      public override string GetPgmdesc( )
      {
         return "Coin Control: choose the coins to spend" ;
      }

      protected void WB3K0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            RenderHtmlHeaders( ) ;
            RenderHtmlOpenForm( ) ;
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", "", "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTotalbalance_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTotalbalance_Internalname, "Balance", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'" + sGXsfl_14_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTotalbalance_Internalname, StringUtil.LTrim( StringUtil.NToC( AV14totalBalance, 16, 8, ".", "")), StringUtil.LTrim( ((edtavTotalbalance_Enabled!=0) ? context.localUtil.Format( AV14totalBalance, "ZZZZZZ9.99999999") : context.localUtil.Format( AV14totalBalance, "ZZZZZZ9.99999999"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,8);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTotalbalance_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavTotalbalance_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/CoinControl.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbselection_Internalname, lblTbselection_Caption, "", "", lblTbselection_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/CoinControl.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            CoinsgridContainer.SetWrapped(nGXWrapped);
            StartGridControl14( ) ;
         }
         if ( wbEnd == 14 )
         {
            wbEnd = 0;
            nRC_GXsfl_14 = (int)(nGXsfl_14_idx-1);
            if ( CoinsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               CoinsgridContainer.AddObjectProperty("COINSGRID_nEOF", COINSGRID_nEOF);
               CoinsgridContainer.AddObjectProperty("COINSGRID_nFirstRecordOnPage", COINSGRID_nFirstRecordOnPage);
               AV17GXV1 = nGXsfl_14_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"CoinsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Coinsgrid", CoinsgridContainer, subCoinsgrid_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "CoinsgridContainerData", CoinsgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "CoinsgridContainerData"+"V", CoinsgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"CoinsgridContainerData"+"V"+"\" value='"+CoinsgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 23,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttUseselectedcoins_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(14), 2, 0)+","+"null"+");", "Use Selected Coins", bttUseselectedcoins_Jsonclick, 5, "Use Selected Coins", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'USE SELECTED COINS\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/CoinControl.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttAutomaticselection_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(14), 2, 0)+","+"null"+");", "Automatic Selection", bttAutomaticselection_Jsonclick, 5, "Automatic Selection", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'AUTOMATIC SELECTION\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/CoinControl.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 14 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( CoinsgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  CoinsgridContainer.AddObjectProperty("COINSGRID_nEOF", COINSGRID_nEOF);
                  CoinsgridContainer.AddObjectProperty("COINSGRID_nFirstRecordOnPage", COINSGRID_nFirstRecordOnPage);
                  AV17GXV1 = nGXsfl_14_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"CoinsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Coinsgrid", CoinsgridContainer, subCoinsgrid_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "CoinsgridContainerData", CoinsgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "CoinsgridContainerData"+"V", CoinsgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"CoinsgridContainerData"+"V"+"\" value='"+CoinsgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START3K2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
            }
         }
         Form.Meta.addItem("description", "Coin Control: choose the coins to spend", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3K0( ) ;
      }

      protected void WS3K2( )
      {
         START3K2( ) ;
         EVT3K2( ) ;
      }

      protected void EVT3K2( )
      {
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               sEvt = cgiGet( "_EventName");
               EvtGridId = cgiGet( "_EventGridId");
               EvtRowId = cgiGet( "_EventRowId");
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
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                        }
                        else if ( StringUtil.StrCmp(sEvt, "'USE SELECTED COINS'") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'Use Selected Coins' */
                           E113K2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "'AUTOMATIC SELECTION'") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'Automatic Selection' */
                           E123K2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                        }
                        else if ( StringUtil.StrCmp(sEvt, "COINSGRIDPAGING") == 0 )
                        {
                           context.wbHandled = 1;
                           sEvt = cgiGet( "COINSGRIDPAGING");
                           if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                           {
                              subcoinsgrid_firstpage( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                           {
                              subcoinsgrid_previouspage( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                           {
                              subcoinsgrid_nextpage( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                           {
                              subcoinsgrid_lastpage( ) ;
                           }
                           dynload_actions( ) ;
                        }
                     }
                     else
                     {
                        sEvtType = StringUtil.Right( sEvt, 4);
                        sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                        if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "COINSGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'TOGGLE COIN'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'TOGGLE COIN'") == 0 ) )
                        {
                           nGXsfl_14_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                           sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
                           SubsflControlProps_142( ) ;
                           AV17GXV1 = (int)(nGXsfl_14_idx+COINSGRID_nFirstRecordOnPage);
                           if ( ( AV6coins.Count >= AV17GXV1 ) && ( AV17GXV1 > 0 ) )
                           {
                              AV6coins.CurrentItem = ((GeneXus.Programs.wallet.SdtCoinControlItem)AV6coins.Item(AV17GXV1));
                              AV16useCoin = cgiGet( edtavUsecoin_Internalname);
                              AssignAttri("", false, edtavUsecoin_Internalname, AV16useCoin);
                              AV5coinLabel = cgiGet( edtavCoinlabel_Internalname);
                              AssignAttri("", false, edtavCoinlabel_Internalname, AV5coinLabel);
                              AV15txtConfirmations = cgiGet( edtavTxtconfirmations_Internalname);
                              AssignAttri("", false, edtavTxtconfirmations_Internalname, AV15txtConfirmations);
                           }
                           sEvtType = StringUtil.Right( sEvt, 1);
                           if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                           {
                              sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                              if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                              {
                                 context.wbHandled = 1;
                                 dynload_actions( ) ;
                                 /* Execute user event: Start */
                                 E133K2 ();
                              }
                              else if ( StringUtil.StrCmp(sEvt, "COINSGRID.LOAD") == 0 )
                              {
                                 context.wbHandled = 1;
                                 dynload_actions( ) ;
                                 /* Execute user event: Coinsgrid.Load */
                                 E143K2 ();
                              }
                              else if ( StringUtil.StrCmp(sEvt, "'TOGGLE COIN'") == 0 )
                              {
                                 context.wbHandled = 1;
                                 dynload_actions( ) ;
                                 /* Execute user event: 'Toggle Coin' */
                                 E153K2 ();
                              }
                              else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    Rfr0gs = false;
                                    if ( ! Rfr0gs )
                                    {
                                    }
                                    dynload_actions( ) ;
                                 }
                                 /* No code required for Cancel button. It is implemented as the Reset button. */
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                              {
                                 context.wbHandled = 1;
                                 dynload_actions( ) ;
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

      protected void WE3K2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm3K2( ) ;
            }
         }
      }

      protected void PA3K2( )
      {
         if ( nDonePA == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            init_web_controls( ) ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = edtavTotalbalance_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrCoinsgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_142( ) ;
         while ( nGXsfl_14_idx <= nRC_GXsfl_14 )
         {
            sendrow_142( ) ;
            nGXsfl_14_idx = ((subCoinsgrid_Islastpage==1)&&(nGXsfl_14_idx+1>subCoinsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_idx+1);
            sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
            SubsflControlProps_142( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( CoinsgridContainer)) ;
         /* End function gxnrCoinsgrid_newrow */
      }

      protected void gxgrCoinsgrid_refresh( int subCoinsgrid_Rows ,
                                            GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem> AV6coins )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         COINSGRID_nCurrentRecord = 0;
         RF3K2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrCoinsgrid_refresh */
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
         RF3K2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavTotalbalance_Enabled = 0;
         AssignProp("", false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
         edtavUsecoin_Enabled = 0;
         edtavCtlamount_Enabled = 0;
         edtavCoinlabel_Enabled = 0;
         edtavTxtconfirmations_Enabled = 0;
         edtavCtlreceiveddatetime_Enabled = 0;
         edtavCtloutpoint_Enabled = 0;
      }

      protected void RF3K2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            CoinsgridContainer.ClearRows();
         }
         wbStart = 14;
         nGXsfl_14_idx = 1;
         sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
         SubsflControlProps_142( ) ;
         bGXsfl_14_Refreshing = true;
         CoinsgridContainer.AddObjectProperty("GridName", "Coinsgrid");
         CoinsgridContainer.AddObjectProperty("CmpContext", "");
         CoinsgridContainer.AddObjectProperty("InMasterPage", "false");
         CoinsgridContainer.AddObjectProperty("Class", "Grid");
         CoinsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         CoinsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         CoinsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCoinsgrid_Backcolorstyle), 1, 0, ".", "")));
         CoinsgridContainer.PageSize = subCoinsgrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_142( ) ;
            /* Execute user event: Coinsgrid.Load */
            E143K2 ();
            if ( ( subCoinsgrid_Islastpage == 0 ) && ( COINSGRID_nCurrentRecord > 0 ) && ( COINSGRID_nGridOutOfScope == 0 ) && ( nGXsfl_14_idx == 1 ) )
            {
               COINSGRID_nCurrentRecord = 0;
               COINSGRID_nGridOutOfScope = 1;
               subcoinsgrid_firstpage( ) ;
               /* Execute user event: Coinsgrid.Load */
               E143K2 ();
            }
            wbEnd = 14;
            WB3K0( ) ;
         }
         bGXsfl_14_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3K2( )
      {
      }

      protected int subCoinsgrid_fnc_Pagecount( )
      {
         COINSGRID_nRecordCount = subCoinsgrid_fnc_Recordcount( );
         if ( ((long)((COINSGRID_nRecordCount) % (subCoinsgrid_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(COINSGRID_nRecordCount/ (decimal)(subCoinsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(COINSGRID_nRecordCount/ (decimal)(subCoinsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subCoinsgrid_fnc_Recordcount( )
      {
         return AV6coins.Count ;
      }

      protected int subCoinsgrid_fnc_Recordsperpage( )
      {
         return (int)(50*1) ;
      }

      protected int subCoinsgrid_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(COINSGRID_nFirstRecordOnPage/ (decimal)(subCoinsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subcoinsgrid_firstpage( )
      {
         COINSGRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "COINSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(COINSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrCoinsgrid_refresh( subCoinsgrid_Rows, AV6coins) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subcoinsgrid_nextpage( )
      {
         COINSGRID_nRecordCount = subCoinsgrid_fnc_Recordcount( );
         if ( ( COINSGRID_nRecordCount >= subCoinsgrid_fnc_Recordsperpage( ) ) && ( COINSGRID_nEOF == 0 ) )
         {
            COINSGRID_nFirstRecordOnPage = (long)(COINSGRID_nFirstRecordOnPage+subCoinsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "COINSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(COINSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         CoinsgridContainer.AddObjectProperty("COINSGRID_nFirstRecordOnPage", COINSGRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrCoinsgrid_refresh( subCoinsgrid_Rows, AV6coins) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((COINSGRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subcoinsgrid_previouspage( )
      {
         if ( COINSGRID_nFirstRecordOnPage >= subCoinsgrid_fnc_Recordsperpage( ) )
         {
            COINSGRID_nFirstRecordOnPage = (long)(COINSGRID_nFirstRecordOnPage-subCoinsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "COINSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(COINSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrCoinsgrid_refresh( subCoinsgrid_Rows, AV6coins) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subcoinsgrid_lastpage( )
      {
         COINSGRID_nRecordCount = subCoinsgrid_fnc_Recordcount( );
         if ( COINSGRID_nRecordCount > subCoinsgrid_fnc_Recordsperpage( ) )
         {
            if ( ((long)((COINSGRID_nRecordCount) % (subCoinsgrid_fnc_Recordsperpage( )))) == 0 )
            {
               COINSGRID_nFirstRecordOnPage = (long)(COINSGRID_nRecordCount-subCoinsgrid_fnc_Recordsperpage( ));
            }
            else
            {
               COINSGRID_nFirstRecordOnPage = (long)(COINSGRID_nRecordCount-((long)((COINSGRID_nRecordCount) % (subCoinsgrid_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            COINSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "COINSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(COINSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrCoinsgrid_refresh( subCoinsgrid_Rows, AV6coins) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subcoinsgrid_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            COINSGRID_nFirstRecordOnPage = (long)(subCoinsgrid_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            COINSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "COINSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(COINSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrCoinsgrid_refresh( subCoinsgrid_Rows, AV6coins) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavTotalbalance_Enabled = 0;
         AssignProp("", false, edtavTotalbalance_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotalbalance_Enabled), 5, 0), true);
         edtavUsecoin_Enabled = 0;
         edtavCtlamount_Enabled = 0;
         edtavCoinlabel_Enabled = 0;
         edtavTxtconfirmations_Enabled = 0;
         edtavCtlreceiveddatetime_Enabled = 0;
         edtavCtloutpoint_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3K0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E133K2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Coins"), AV6coins);
            ajax_req_read_hidden_sdt(cgiGet( "vCOINS"), AV6coins);
            /* Read saved values. */
            nRC_GXsfl_14 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_14"), ".", ","), 18, MidpointRounding.ToEven));
            COINSGRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "COINSGRID_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            COINSGRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( "COINSGRID_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_14 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_14"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_14_fel_idx = 0;
            while ( nGXsfl_14_fel_idx < nRC_GXsfl_14 )
            {
               nGXsfl_14_fel_idx = ((subCoinsgrid_Islastpage==1)&&(nGXsfl_14_fel_idx+1>subCoinsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_fel_idx+1);
               sGXsfl_14_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_142( ) ;
               AV17GXV1 = (int)(nGXsfl_14_fel_idx+COINSGRID_nFirstRecordOnPage);
               if ( ( AV6coins.Count >= AV17GXV1 ) && ( AV17GXV1 > 0 ) )
               {
                  AV6coins.CurrentItem = ((GeneXus.Programs.wallet.SdtCoinControlItem)AV6coins.Item(AV17GXV1));
                  AV16useCoin = cgiGet( edtavUsecoin_Internalname);
                  AV5coinLabel = cgiGet( edtavCoinlabel_Internalname);
                  AV15txtConfirmations = cgiGet( edtavTxtconfirmations_Internalname);
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
            if ( ( ( context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vTOTALBALANCE");
               GX_FocusControl = edtavTotalbalance_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV14totalBalance = 0;
               AssignAttri("", false, "AV14totalBalance", StringUtil.LTrimStr( AV14totalBalance, 16, 8));
            }
            else
            {
               AV14totalBalance = context.localUtil.CToN( cgiGet( edtavTotalbalance_Internalname), ".", ",");
               AssignAttri("", false, "AV14totalBalance", StringUtil.LTrimStr( AV14totalBalance, 16, 8));
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

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E133K2 ();
         if (returnInSub) return;
      }

      protected void E133K2( )
      {
         /* Start Routine */
         returnInSub = false;
         /* Execute user subroutine: 'LOAD COINS' */
         S112 ();
         if (returnInSub) return;
      }

      private void E143K2( )
      {
         /* Coinsgrid_Load Routine */
         returnInSub = false;
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV6coins.Count )
         {
            AV6coins.CurrentItem = ((GeneXus.Programs.wallet.SdtCoinControlItem)AV6coins.Item(AV17GXV1));
            if ( ((GeneXus.Programs.wallet.SdtCoinControlItem)(AV6coins.CurrentItem)).gxTpr_Selected )
            {
               AV16useCoin = "Selected";
               AssignAttri("", false, edtavUsecoin_Internalname, AV16useCoin);
            }
            else
            {
               AV16useCoin = "Select";
               AssignAttri("", false, edtavUsecoin_Internalname, AV16useCoin);
            }
            if ( ((GeneXus.Programs.wallet.SdtCoinControlItem)(AV6coins.CurrentItem)).gxTpr_Confirmations >= 6 )
            {
               AV15txtConfirmations = "CONFIRMED";
               AssignAttri("", false, edtavTxtconfirmations_Internalname, AV15txtConfirmations);
            }
            else if ( ((GeneXus.Programs.wallet.SdtCoinControlItem)(AV6coins.CurrentItem)).gxTpr_Confirmations == 0 )
            {
               AV15txtConfirmations = "IN MEMPOOL";
               AssignAttri("", false, edtavTxtconfirmations_Internalname, AV15txtConfirmations);
            }
            else
            {
               AV15txtConfirmations = StringUtil.Trim( StringUtil.Str( (decimal)(((GeneXus.Programs.wallet.SdtCoinControlItem)(AV6coins.CurrentItem)).gxTpr_Confirmations), 10, 0));
               AssignAttri("", false, edtavTxtconfirmations_Internalname, AV15txtConfirmations);
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtCoinControlItem)(AV6coins.CurrentItem)).gxTpr_Description)) )
            {
               AV5coinLabel = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtCoinControlItem)(AV6coins.CurrentItem)).gxTpr_Address);
               AssignAttri("", false, edtavCoinlabel_Internalname, AV5coinLabel);
            }
            else
            {
               AV5coinLabel = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtCoinControlItem)(AV6coins.CurrentItem)).gxTpr_Description) + " (" + StringUtil.Trim( ((GeneXus.Programs.wallet.SdtCoinControlItem)(AV6coins.CurrentItem)).gxTpr_Address) + ")";
               AssignAttri("", false, edtavCoinlabel_Internalname, AV5coinLabel);
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 14;
            }
            if ( ( subCoinsgrid_Islastpage == 1 ) || ( 50 == 0 ) || ( ( COINSGRID_nCurrentRecord >= COINSGRID_nFirstRecordOnPage ) && ( COINSGRID_nCurrentRecord < COINSGRID_nFirstRecordOnPage + subCoinsgrid_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_142( ) ;
            }
            COINSGRID_nEOF = (short)(((subCoinsgrid_Rows==0)||(COINSGRID_nCurrentRecord<COINSGRID_nFirstRecordOnPage+subCoinsgrid_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "COINSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(COINSGRID_nEOF), 1, 0, ".", "")));
            COINSGRID_nCurrentRecord = (long)(COINSGRID_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_14_Refreshing )
            {
               DoAjaxLoad(14, CoinsgridRow);
            }
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E153K2( )
      {
         AV17GXV1 = (int)(nGXsfl_14_idx+COINSGRID_nFirstRecordOnPage);
         if ( ( AV17GXV1 > 0 ) && ( AV6coins.Count >= AV17GXV1 ) )
         {
            AV6coins.CurrentItem = ((GeneXus.Programs.wallet.SdtCoinControlItem)AV6coins.Item(AV17GXV1));
         }
         /* 'Toggle Coin' Routine */
         returnInSub = false;
         AV10outpoint = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtCoinControlItem)(AV6coins.CurrentItem)).gxTpr_Outpoint);
         GXt_objcol_svchar1 = AV11selected;
         new GeneXus.Programs.wallet.getselectedcoins(context ).execute( out  GXt_objcol_svchar1) ;
         AV11selected = GXt_objcol_svchar1;
         AV8newSelection = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV7found = false;
         AV21GXV5 = 1;
         while ( AV21GXV5 <= AV11selected.Count )
         {
            AV9oneOutpoint = ((string)AV11selected.Item(AV21GXV5));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV9oneOutpoint), AV10outpoint) == 0 )
            {
               AV7found = true;
            }
            else
            {
               AV8newSelection.Add(StringUtil.Trim( AV9oneOutpoint), 0);
            }
            AV21GXV5 = (int)(AV21GXV5+1);
         }
         if ( ! AV7found )
         {
            AV8newSelection.Add(AV10outpoint, 0);
         }
         new GeneXus.Programs.wallet.setselectedcoins(context ).execute(  AV8newSelection) ;
         /* Execute user subroutine: 'LOAD COINS' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV6coins", AV6coins);
         nGXsfl_14_bak_idx = nGXsfl_14_idx;
         gxgrCoinsgrid_refresh( subCoinsgrid_Rows, AV6coins) ;
         nGXsfl_14_idx = nGXsfl_14_bak_idx;
         sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
         SubsflControlProps_142( ) ;
      }

      protected void E113K2( )
      {
         /* 'Use Selected Coins' Routine */
         returnInSub = false;
         if ( AV12selectedCount == 0 )
         {
            this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"No coin selected",(string)"Select at least one coin, or choose Automatic selection"}, true);
         }
         else
         {
            context.setWebReturnParms(new Object[] {});
            context.setWebReturnParmsMetadata(new Object[] {});
            context.wjLocDisableFrm = 1;
            context.nUserReturn = 1;
            returnInSub = true;
            if (true) return;
         }
      }

      protected void E123K2( )
      {
         /* 'Automatic Selection' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.clearselectedcoins(context ).execute( ) ;
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      protected void S112( )
      {
         /* 'LOAD COINS' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.getcoincontrolcoins(context ).execute( out  AV6coins, out  AV14totalBalance, out  AV12selectedCount, out  AV13selectedTotal) ;
         gx_BV14 = true;
         AssignAttri("", false, "AV14totalBalance", StringUtil.LTrimStr( AV14totalBalance, 16, 8));
         AssignAttri("", false, "AV12selectedCount", StringUtil.LTrimStr( (decimal)(AV12selectedCount), 10, 0));
         if ( AV12selectedCount == 0 )
         {
            lblTbselection_Caption = "Automatic selection: the wallet chooses the coins. Select coins to choose them yourself.";
            AssignProp("", false, lblTbselection_Internalname, "Caption", lblTbselection_Caption, true);
         }
         else
         {
            lblTbselection_Caption = StringUtil.Trim( StringUtil.Str( (decimal)(AV12selectedCount), 10, 0))+" coin(s) selected: "+StringUtil.Trim( StringUtil.Str( AV13selectedTotal, 16, 8))+" BTC. Only these coins will be spent; they must cover the amount and the fee.";
            AssignProp("", false, lblTbselection_Internalname, "Caption", lblTbselection_Caption, true);
         }
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
         PA3K2( ) ;
         WS3K2( ) ;
         WE3K2( ) ;
         cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2026103013274216", true, true, false);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         context.AddJavascriptSource("gxdec.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("wallet/coincontrol.js", "?2026103013274216", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_142( )
      {
         edtavUsecoin_Internalname = "vUSECOIN_"+sGXsfl_14_idx;
         edtavCtlamount_Internalname = "CTLAMOUNT_"+sGXsfl_14_idx;
         edtavCoinlabel_Internalname = "vCOINLABEL_"+sGXsfl_14_idx;
         edtavTxtconfirmations_Internalname = "vTXTCONFIRMATIONS_"+sGXsfl_14_idx;
         edtavCtlreceiveddatetime_Internalname = "CTLRECEIVEDDATETIME_"+sGXsfl_14_idx;
         edtavCtloutpoint_Internalname = "CTLOUTPOINT_"+sGXsfl_14_idx;
      }

      protected void SubsflControlProps_fel_142( )
      {
         edtavUsecoin_Internalname = "vUSECOIN_"+sGXsfl_14_fel_idx;
         edtavCtlamount_Internalname = "CTLAMOUNT_"+sGXsfl_14_fel_idx;
         edtavCoinlabel_Internalname = "vCOINLABEL_"+sGXsfl_14_fel_idx;
         edtavTxtconfirmations_Internalname = "vTXTCONFIRMATIONS_"+sGXsfl_14_fel_idx;
         edtavCtlreceiveddatetime_Internalname = "CTLRECEIVEDDATETIME_"+sGXsfl_14_fel_idx;
         edtavCtloutpoint_Internalname = "CTLOUTPOINT_"+sGXsfl_14_fel_idx;
      }

      protected void sendrow_142( )
      {
         sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
         SubsflControlProps_142( ) ;
         WB3K0( ) ;
         if ( ( 50 * 1 == 0 ) || ( nGXsfl_14_idx <= subCoinsgrid_fnc_Recordsperpage( ) * 1 ) )
         {
            CoinsgridRow = GXWebRow.GetNew(context,CoinsgridContainer);
            if ( subCoinsgrid_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subCoinsgrid_Backstyle = 0;
               if ( StringUtil.StrCmp(subCoinsgrid_Class, "") != 0 )
               {
                  subCoinsgrid_Linesclass = subCoinsgrid_Class+"Odd";
               }
            }
            else if ( subCoinsgrid_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subCoinsgrid_Backstyle = 0;
               subCoinsgrid_Backcolor = subCoinsgrid_Allbackcolor;
               if ( StringUtil.StrCmp(subCoinsgrid_Class, "") != 0 )
               {
                  subCoinsgrid_Linesclass = subCoinsgrid_Class+"Uniform";
               }
            }
            else if ( subCoinsgrid_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subCoinsgrid_Backstyle = 1;
               if ( StringUtil.StrCmp(subCoinsgrid_Class, "") != 0 )
               {
                  subCoinsgrid_Linesclass = subCoinsgrid_Class+"Odd";
               }
               subCoinsgrid_Backcolor = (int)(0x0);
            }
            else if ( subCoinsgrid_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subCoinsgrid_Backstyle = 1;
               if ( ((int)((nGXsfl_14_idx) % (2))) == 0 )
               {
                  subCoinsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subCoinsgrid_Class, "") != 0 )
                  {
                     subCoinsgrid_Linesclass = subCoinsgrid_Class+"Even";
                  }
               }
               else
               {
                  subCoinsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subCoinsgrid_Class, "") != 0 )
                  {
                     subCoinsgrid_Linesclass = subCoinsgrid_Class+"Odd";
                  }
               }
            }
            if ( CoinsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_14_idx+"\">") ;
            }
            /* Subfile cell */
            if ( CoinsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            CoinsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavUsecoin_Internalname,StringUtil.RTrim( AV16useCoin),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,15);\"","'"+""+"'"+",false,"+"'"+"E\\'TOGGLE COIN\\'."+sGXsfl_14_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavUsecoin_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavUsecoin_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( CoinsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            CoinsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlamount_Internalname,StringUtil.LTrim( StringUtil.NToC( ((GeneXus.Programs.wallet.SdtCoinControlItem)AV6coins.Item(AV17GXV1)).gxTpr_Amount, 16, 8, ".", "")),StringUtil.LTrim( ((edtavCtlamount_Enabled!=0) ? context.localUtil.Format( ((GeneXus.Programs.wallet.SdtCoinControlItem)AV6coins.Item(AV17GXV1)).gxTpr_Amount, "ZZZZZZ9.99999999") : context.localUtil.Format( ((GeneXus.Programs.wallet.SdtCoinControlItem)AV6coins.Item(AV17GXV1)).gxTpr_Amount, "ZZZZZZ9.99999999"))),TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,16);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlamount_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlamount_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)16,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( CoinsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            CoinsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCoinlabel_Internalname,(string)AV5coinLabel,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,17);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCoinlabel_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCoinlabel_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)400,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( CoinsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            CoinsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavTxtconfirmations_Internalname,StringUtil.RTrim( AV15txtConfirmations),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavTxtconfirmations_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavTxtconfirmations_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( CoinsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'',false,'" + sGXsfl_14_idx + "',14)\"";
            ROClassString = "Attribute";
            CoinsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlreceiveddatetime_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.SdtCoinControlItem)AV6coins.Item(AV17GXV1)).gxTpr_Receiveddatetime, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.SdtCoinControlItem)AV6coins.Item(AV17GXV1)).gxTpr_Receiveddatetime, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onblur(this,19);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlreceiveddatetime_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlreceiveddatetime_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( CoinsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            CoinsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtloutpoint_Internalname,((GeneXus.Programs.wallet.SdtCoinControlItem)AV6coins.Item(AV17GXV1)).gxTpr_Outpoint,(string)"",""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtloutpoint_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtloutpoint_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes3K2( ) ;
            CoinsgridContainer.AddRow(CoinsgridRow);
            nGXsfl_14_idx = ((subCoinsgrid_Islastpage==1)&&(nGXsfl_14_idx+1>subCoinsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_idx+1);
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
         if ( CoinsgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"CoinsgridContainer"+"DivS\" data-gxgridid=\"14\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subCoinsgrid_Internalname, subCoinsgrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subCoinsgrid_Backcolorstyle == 0 )
            {
               subCoinsgrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subCoinsgrid_Class) > 0 )
               {
                  subCoinsgrid_Linesclass = subCoinsgrid_Class+"Title";
               }
            }
            else
            {
               subCoinsgrid_Titlebackstyle = 1;
               if ( subCoinsgrid_Backcolorstyle == 1 )
               {
                  subCoinsgrid_Titlebackcolor = subCoinsgrid_Allbackcolor;
                  if ( StringUtil.Len( subCoinsgrid_Class) > 0 )
                  {
                     subCoinsgrid_Linesclass = subCoinsgrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subCoinsgrid_Class) > 0 )
                  {
                     subCoinsgrid_Linesclass = subCoinsgrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Use") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Amount") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Address / description") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Confirmations") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Received") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "outpoint") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            CoinsgridContainer.AddObjectProperty("GridName", "Coinsgrid");
         }
         else
         {
            CoinsgridContainer.AddObjectProperty("GridName", "Coinsgrid");
            CoinsgridContainer.AddObjectProperty("Header", subCoinsgrid_Header);
            CoinsgridContainer.AddObjectProperty("Class", "Grid");
            CoinsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            CoinsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            CoinsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCoinsgrid_Backcolorstyle), 1, 0, ".", "")));
            CoinsgridContainer.AddObjectProperty("CmpContext", "");
            CoinsgridContainer.AddObjectProperty("InMasterPage", "false");
            CoinsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CoinsgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV16useCoin)));
            CoinsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavUsecoin_Enabled), 5, 0, ".", "")));
            CoinsgridContainer.AddColumnProperties(CoinsgridColumn);
            CoinsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CoinsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlamount_Enabled), 5, 0, ".", "")));
            CoinsgridContainer.AddColumnProperties(CoinsgridColumn);
            CoinsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CoinsgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV5coinLabel));
            CoinsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCoinlabel_Enabled), 5, 0, ".", "")));
            CoinsgridContainer.AddColumnProperties(CoinsgridColumn);
            CoinsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CoinsgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV15txtConfirmations)));
            CoinsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavTxtconfirmations_Enabled), 5, 0, ".", "")));
            CoinsgridContainer.AddColumnProperties(CoinsgridColumn);
            CoinsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CoinsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlreceiveddatetime_Enabled), 5, 0, ".", "")));
            CoinsgridContainer.AddColumnProperties(CoinsgridColumn);
            CoinsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CoinsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtloutpoint_Enabled), 5, 0, ".", "")));
            CoinsgridContainer.AddColumnProperties(CoinsgridColumn);
            CoinsgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCoinsgrid_Selectedindex), 4, 0, ".", "")));
            CoinsgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCoinsgrid_Allowselection), 1, 0, ".", "")));
            CoinsgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCoinsgrid_Selectioncolor), 9, 0, ".", "")));
            CoinsgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCoinsgrid_Allowhovering), 1, 0, ".", "")));
            CoinsgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCoinsgrid_Hoveringcolor), 9, 0, ".", "")));
            CoinsgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCoinsgrid_Allowcollapsing), 1, 0, ".", "")));
            CoinsgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCoinsgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         edtavTotalbalance_Internalname = "vTOTALBALANCE";
         lblTbselection_Internalname = "TBSELECTION";
         edtavUsecoin_Internalname = "vUSECOIN";
         edtavCtlamount_Internalname = "CTLAMOUNT";
         edtavCoinlabel_Internalname = "vCOINLABEL";
         edtavTxtconfirmations_Internalname = "vTXTCONFIRMATIONS";
         edtavCtlreceiveddatetime_Internalname = "CTLRECEIVEDDATETIME";
         edtavCtloutpoint_Internalname = "CTLOUTPOINT";
         bttUseselectedcoins_Internalname = "USESELECTEDCOINS";
         bttAutomaticselection_Internalname = "AUTOMATICSELECTION";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subCoinsgrid_Internalname = "COINSGRID";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subCoinsgrid_Allowcollapsing = 0;
         subCoinsgrid_Allowselection = 0;
         subCoinsgrid_Header = "";
         edtavCtloutpoint_Jsonclick = "";
         edtavCtloutpoint_Enabled = 0;
         edtavCtlreceiveddatetime_Jsonclick = "";
         edtavCtlreceiveddatetime_Enabled = 0;
         edtavTxtconfirmations_Jsonclick = "";
         edtavTxtconfirmations_Enabled = 1;
         edtavCoinlabel_Jsonclick = "";
         edtavCoinlabel_Enabled = 1;
         edtavCtlamount_Jsonclick = "";
         edtavCtlamount_Enabled = 0;
         edtavUsecoin_Jsonclick = "";
         edtavUsecoin_Enabled = 1;
         subCoinsgrid_Class = "Grid";
         subCoinsgrid_Backcolorstyle = 0;
         lblTbselection_Caption = "Selection";
         edtavTotalbalance_Jsonclick = "";
         edtavTotalbalance_Enabled = 1;
         edtavCtloutpoint_Enabled = -1;
         edtavCtlreceiveddatetime_Enabled = -1;
         edtavCtlamount_Enabled = -1;
         subCoinsgrid_Rows = 50;
         if ( context.isSpaRequest( ) )
         {
            enableJsOutput();
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"COINSGRID_nFirstRecordOnPage","type":"int"},{"av":"COINSGRID_nEOF","type":"int"},{"av":"subCoinsgrid_Rows","ctrl":"COINSGRID","prop":"Rows"},{"av":"AV6coins","fld":"vCOINS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"COINSGRID","prop":"GridRC","grid":14,"type":"int"}]}""");
         setEventMetadata("COINSGRID.LOAD","""{"handler":"E143K2","iparms":[{"av":"AV6coins","fld":"vCOINS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"COINSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"COINSGRID","prop":"GridRC","grid":14,"type":"int"}]""");
         setEventMetadata("COINSGRID.LOAD",""","oparms":[{"av":"AV16useCoin","fld":"vUSECOIN","type":"char"},{"av":"AV15txtConfirmations","fld":"vTXTCONFIRMATIONS","type":"char"},{"av":"AV5coinLabel","fld":"vCOINLABEL","type":"svchar"}]}""");
         setEventMetadata("'TOGGLE COIN'","""{"handler":"E153K2","iparms":[{"av":"AV6coins","fld":"vCOINS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"COINSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"COINSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"COINSGRID_nEOF","type":"int"},{"av":"subCoinsgrid_Rows","ctrl":"COINSGRID","prop":"Rows"}]""");
         setEventMetadata("'TOGGLE COIN'",""","oparms":[{"av":"AV12selectedCount","fld":"vSELECTEDCOUNT","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV14totalBalance","fld":"vTOTALBALANCE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV6coins","fld":"vCOINS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"COINSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_14","ctrl":"COINSGRID","prop":"GridRC","grid":14,"type":"int"},{"av":"lblTbselection_Caption","ctrl":"TBSELECTION","prop":"Caption"}]}""");
         setEventMetadata("'USE SELECTED COINS'","""{"handler":"E113K2","iparms":[{"av":"AV12selectedCount","fld":"vSELECTEDCOUNT","pic":"ZZZZZZZZZ9","type":"int"}]}""");
         setEventMetadata("'AUTOMATIC SELECTION'","""{"handler":"E123K2","iparms":[]}""");
         setEventMetadata("COINSGRID_FIRSTPAGE","""{"handler":"subcoinsgrid_firstpage","iparms":[{"av":"COINSGRID_nFirstRecordOnPage","type":"int"},{"av":"COINSGRID_nEOF","type":"int"},{"av":"subCoinsgrid_Rows","ctrl":"COINSGRID","prop":"Rows"},{"av":"AV6coins","fld":"vCOINS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"COINSGRID","prop":"GridRC","grid":14,"type":"int"}]}""");
         setEventMetadata("COINSGRID_PREVPAGE","""{"handler":"subcoinsgrid_previouspage","iparms":[{"av":"COINSGRID_nFirstRecordOnPage","type":"int"},{"av":"COINSGRID_nEOF","type":"int"},{"av":"subCoinsgrid_Rows","ctrl":"COINSGRID","prop":"Rows"},{"av":"AV6coins","fld":"vCOINS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"COINSGRID","prop":"GridRC","grid":14,"type":"int"}]}""");
         setEventMetadata("COINSGRID_NEXTPAGE","""{"handler":"subcoinsgrid_nextpage","iparms":[{"av":"COINSGRID_nFirstRecordOnPage","type":"int"},{"av":"COINSGRID_nEOF","type":"int"},{"av":"subCoinsgrid_Rows","ctrl":"COINSGRID","prop":"Rows"},{"av":"AV6coins","fld":"vCOINS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"COINSGRID","prop":"GridRC","grid":14,"type":"int"}]}""");
         setEventMetadata("COINSGRID_LASTPAGE","""{"handler":"subcoinsgrid_lastpage","iparms":[{"av":"COINSGRID_nFirstRecordOnPage","type":"int"},{"av":"COINSGRID_nEOF","type":"int"},{"av":"subCoinsgrid_Rows","ctrl":"COINSGRID","prop":"Rows"},{"av":"AV6coins","fld":"vCOINS","grid":14,"type":""},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"COINSGRID","prop":"GridRC","grid":14,"type":"int"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv4","iparms":[]}""");
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
         AV6coins = new GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem>( context, "CoinControlItem", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GX_FocusControl = "";
         sPrefix = "";
         TempTags = "";
         lblTbselection_Jsonclick = "";
         CoinsgridContainer = new GXWebGrid( context);
         sStyleString = "";
         ClassString = "";
         StyleString = "";
         bttUseselectedcoins_Jsonclick = "";
         bttAutomaticselection_Jsonclick = "";
         Form = new GXWebForm();
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV16useCoin = "";
         AV5coinLabel = "";
         AV15txtConfirmations = "";
         CoinsgridRow = new GXWebRow();
         AV10outpoint = "";
         AV11selected = new GxSimpleCollection<string>();
         GXt_objcol_svchar1 = new GxSimpleCollection<string>();
         AV8newSelection = new GxSimpleCollection<string>();
         AV9oneOutpoint = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subCoinsgrid_Linesclass = "";
         ROClassString = "";
         CoinsgridColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavTotalbalance_Enabled = 0;
         edtavUsecoin_Enabled = 0;
         edtavCtlamount_Enabled = 0;
         edtavCoinlabel_Enabled = 0;
         edtavTxtconfirmations_Enabled = 0;
         edtavCtlreceiveddatetime_Enabled = 0;
         edtavCtloutpoint_Enabled = 0;
      }

      private short COINSGRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subCoinsgrid_Backcolorstyle ;
      private short nGXWrapped ;
      private short subCoinsgrid_Backstyle ;
      private short subCoinsgrid_Titlebackstyle ;
      private short subCoinsgrid_Allowselection ;
      private short subCoinsgrid_Allowhovering ;
      private short subCoinsgrid_Allowcollapsing ;
      private short subCoinsgrid_Collapsed ;
      private int nRC_GXsfl_14 ;
      private int subCoinsgrid_Rows ;
      private int nGXsfl_14_idx=1 ;
      private int edtavTotalbalance_Enabled ;
      private int edtavUsecoin_Enabled ;
      private int edtavCtlamount_Enabled ;
      private int edtavCoinlabel_Enabled ;
      private int edtavTxtconfirmations_Enabled ;
      private int edtavCtlreceiveddatetime_Enabled ;
      private int edtavCtloutpoint_Enabled ;
      private int AV17GXV1 ;
      private int subCoinsgrid_Islastpage ;
      private int COINSGRID_nGridOutOfScope ;
      private int nGXsfl_14_fel_idx=1 ;
      private int AV21GXV5 ;
      private int nGXsfl_14_bak_idx=1 ;
      private int idxLst ;
      private int subCoinsgrid_Backcolor ;
      private int subCoinsgrid_Allbackcolor ;
      private int subCoinsgrid_Titlebackcolor ;
      private int subCoinsgrid_Selectedindex ;
      private int subCoinsgrid_Selectioncolor ;
      private int subCoinsgrid_Hoveringcolor ;
      private long COINSGRID_nFirstRecordOnPage ;
      private long AV12selectedCount ;
      private long COINSGRID_nCurrentRecord ;
      private long COINSGRID_nRecordCount ;
      private decimal AV14totalBalance ;
      private decimal AV13selectedTotal ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_14_idx="0001" ;
      private string edtavTotalbalance_Internalname ;
      private string edtavUsecoin_Internalname ;
      private string edtavCtlamount_Internalname ;
      private string edtavCoinlabel_Internalname ;
      private string edtavTxtconfirmations_Internalname ;
      private string edtavCtlreceiveddatetime_Internalname ;
      private string edtavCtloutpoint_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string edtavTotalbalance_Jsonclick ;
      private string lblTbselection_Internalname ;
      private string lblTbselection_Caption ;
      private string lblTbselection_Jsonclick ;
      private string sStyleString ;
      private string subCoinsgrid_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string bttUseselectedcoins_Internalname ;
      private string bttUseselectedcoins_Jsonclick ;
      private string bttAutomaticselection_Internalname ;
      private string bttAutomaticselection_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV16useCoin ;
      private string AV15txtConfirmations ;
      private string sGXsfl_14_fel_idx="0001" ;
      private string subCoinsgrid_Class ;
      private string subCoinsgrid_Linesclass ;
      private string ROClassString ;
      private string edtavUsecoin_Jsonclick ;
      private string edtavCtlamount_Jsonclick ;
      private string edtavCoinlabel_Jsonclick ;
      private string edtavTxtconfirmations_Jsonclick ;
      private string edtavCtlreceiveddatetime_Jsonclick ;
      private string edtavCtloutpoint_Jsonclick ;
      private string subCoinsgrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_14_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV7found ;
      private bool gx_BV14 ;
      private string AV5coinLabel ;
      private string AV10outpoint ;
      private string AV9oneOutpoint ;
      private GXWebGrid CoinsgridContainer ;
      private GXWebRow CoinsgridRow ;
      private GXWebColumn CoinsgridColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtCoinControlItem> AV6coins ;
      private GxSimpleCollection<string> AV11selected ;
      private GxSimpleCollection<string> GXt_objcol_svchar1 ;
      private GxSimpleCollection<string> AV8newSelection ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
