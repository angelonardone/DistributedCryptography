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
   public class autheticators : GXDataArea
   {
      public autheticators( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public autheticators( IGxContext context )
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridauthenticators") == 0 )
            {
               gxnrGridauthenticators_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridauthenticators") == 0 )
            {
               gxgrGridauthenticators_refresh_invoke( ) ;
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

      protected void gxnrGridauthenticators_newrow_invoke( )
      {
         nRC_GXsfl_9 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_9"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_9_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_9_idx = GetPar( "sGXsfl_9_idx");
         AV10delete = GetPar( "delete");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridauthenticators_newrow( ) ;
         /* End function gxnrGridauthenticators_newrow_invoke */
      }

      protected void gxgrGridauthenticators_refresh_invoke( )
      {
         AV10delete = GetPar( "delete");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridauthenticators_refresh( AV10delete) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridauthenticators_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            MasterPageObj = (GXMasterPage) ClassLoader.GetInstance("general.ui.masterunanimosidebar", "GeneXus.Programs.general.ui.masterunanimosidebar", new Object[] {context});
            MasterPageObj.setDataArea(this,false);
            ValidateSpaRequest();
            MasterPageObj.webExecute();
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

      public override short ExecuteStartEvent( )
      {
         PA122( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START122( ) ;
         }
         return gxajaxcallmode ;
      }

      public override void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      public override void RenderHtmlOpenForm( )
      {
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( Form.Caption) ;
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
         if ( nGXWrapped != 1 )
         {
            MasterPageObj.master_styles();
         }
         CloseStyles();
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 123260), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.WriteHtmlText( Form.Headerrawhtml) ;
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
         bodyStyle = "" + "background-color:" + context.BuildHTMLColor( Form.Backcolor) + ";color:" + context.BuildHTMLColor( Form.Textcolor) + ";";
         if ( nGXWrapped == 0 )
         {
            bodyStyle += "-moz-opacity:0;opacity:0;";
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( Form.Background)) ) )
         {
            bodyStyle += " background-image:url(" + context.convertURL( Form.Background) + ")";
         }
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.autheticators") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Authenticators", AV5authenticators);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Authenticators", AV5authenticators);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_9", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_9), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "vPOPUPNAME", StringUtil.RTrim( AV8PopupName));
         GxWebStd.gx_hidden_field( context, "vERROR", StringUtil.RTrim( AV6error));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vAUTHENTICATORS", AV5authenticators);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vAUTHENTICATORS", AV5authenticators);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vUSERRESPONSE", AV12UserResponse);
      }

      public override void RenderHtmlCloseForm( )
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
      }

      public override void RenderHtmlContent( )
      {
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE122( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT122( ) ;
      }

      public override bool HasEnterEvent( )
      {
         return false ;
      }

      public override GXWebForm GetForm( )
      {
         return Form ;
      }

      public override string GetSelfLink( )
      {
         return formatLink("wallet.autheticators")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.Autheticators" ;
      }

      public override string GetPgmdesc( )
      {
         return "Autheticators" ;
      }

      protected void WB120( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( nGXWrapped == 1 )
            {
               RenderHtmlHeaders( ) ;
               RenderHtmlOpenForm( ) ;
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", "", "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 6,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttImportanewauthenticatorcode_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(9), 1, 0)+","+"null"+");", "Import a new Authenticator Code", bttImportanewauthenticatorcode_Jsonclick, 7, "Import a new Authenticator Code", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"e11121_client"+"'", TempTags, "", 2, "HLP_Wallet/Autheticators.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridauthenticatorsContainer.SetWrapped(nGXWrapped);
            StartGridControl9( ) ;
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            nRC_GXsfl_9 = (int)(nGXsfl_9_idx-1);
            if ( GridauthenticatorsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV14GXV1 = nGXsfl_9_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"GridauthenticatorsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridauthenticators", GridauthenticatorsContainer, subGridauthenticators_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridauthenticatorsContainerData", GridauthenticatorsContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridauthenticatorsContainerData"+"V", GridauthenticatorsContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridauthenticatorsContainerData"+"V"+"\" value='"+GridauthenticatorsContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridauthenticatorsContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV14GXV1 = nGXsfl_9_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"GridauthenticatorsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridauthenticators", GridauthenticatorsContainer, subGridauthenticators_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridauthenticatorsContainerData", GridauthenticatorsContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridauthenticatorsContainerData"+"V", GridauthenticatorsContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridauthenticatorsContainerData"+"V"+"\" value='"+GridauthenticatorsContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START122( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_12-186073", 0) ;
            }
         }
         Form.Meta.addItem("description", "Autheticators", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP120( ) ;
      }

      protected void WS122( )
      {
         START122( ) ;
         EVT122( ) ;
      }

      protected void EVT122( )
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
                  if ( StringUtil.StrCmp(sEvtType, "M") != 0 )
                  {
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
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E12122 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E13122 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 9), "'GET PIN'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "'DELETE AUTHENTICATOR'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 23), "GRIDAUTHENTICATORS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 9), "'GET PIN'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "'DELETE AUTHENTICATOR'") == 0 ) )
                           {
                              nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
                              SubsflControlProps_92( ) ;
                              AV14GXV1 = nGXsfl_9_idx;
                              if ( ( AV5authenticators.Count >= AV14GXV1 ) && ( AV14GXV1 > 0 ) )
                              {
                                 AV5authenticators.CurrentItem = ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV14GXV1));
                                 AV10delete = cgiGet( edtavDelete_Internalname);
                                 AssignAttri("", false, edtavDelete_Internalname, AV10delete);
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
                                    E14122 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'GET PIN'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Get Pin' */
                                    E15122 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DELETE AUTHENTICATOR'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Delete Authenticator' */
                                    E16122 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDAUTHENTICATORS.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridauthenticators.Load */
                                    E17122 ();
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
      }

      protected void WE122( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               if ( nGXWrapped == 1 )
               {
                  RenderHtmlCloseForm( ) ;
               }
            }
         }
      }

      protected void PA122( )
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
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridauthenticators_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_92( ) ;
         while ( nGXsfl_9_idx <= nRC_GXsfl_9 )
         {
            sendrow_92( ) ;
            nGXsfl_9_idx = ((subGridauthenticators_Islastpage==1)&&(nGXsfl_9_idx+1>subGridauthenticators_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridauthenticatorsContainer)) ;
         /* End function gxnrGridauthenticators_newrow */
      }

      protected void gxgrGridauthenticators_refresh( string AV10delete )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDAUTHENTICATORS_nCurrentRecord = 0;
         RF122( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridauthenticators_refresh */
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
         RF122( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlissuer_Enabled = 0;
         edtavCtlaccounttitle_Enabled = 0;
         edtavCtlaccounttitle1_Enabled = 0;
         edtavDelete_Enabled = 0;
      }

      protected void RF122( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridauthenticatorsContainer.ClearRows();
         }
         wbStart = 9;
         nGXsfl_9_idx = 1;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         bGXsfl_9_Refreshing = true;
         GridauthenticatorsContainer.AddObjectProperty("GridName", "Gridauthenticators");
         GridauthenticatorsContainer.AddObjectProperty("CmpContext", "");
         GridauthenticatorsContainer.AddObjectProperty("InMasterPage", "false");
         GridauthenticatorsContainer.AddObjectProperty("Class", "Grid");
         GridauthenticatorsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridauthenticatorsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridauthenticatorsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthenticators_Backcolorstyle), 1, 0, ".", "")));
         GridauthenticatorsContainer.PageSize = subGridauthenticators_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_92( ) ;
            /* Execute user event: Gridauthenticators.Load */
            E17122 ();
            wbEnd = 9;
            WB120( ) ;
         }
         bGXsfl_9_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes122( )
      {
      }

      protected int subGridauthenticators_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridauthenticators_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridauthenticators_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridauthenticators_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtlissuer_Enabled = 0;
         edtavCtlaccounttitle_Enabled = 0;
         edtavCtlaccounttitle1_Enabled = 0;
         edtavDelete_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP120( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E14122 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Authenticators"), AV5authenticators);
            ajax_req_read_hidden_sdt(cgiGet( "vAUTHENTICATORS"), AV5authenticators);
            /* Read saved values. */
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_9"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_9_fel_idx = 0;
            while ( nGXsfl_9_fel_idx < nRC_GXsfl_9 )
            {
               nGXsfl_9_fel_idx = ((subGridauthenticators_Islastpage==1)&&(nGXsfl_9_fel_idx+1>subGridauthenticators_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_fel_idx+1);
               sGXsfl_9_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_92( ) ;
               AV14GXV1 = nGXsfl_9_fel_idx;
               if ( ( AV5authenticators.Count >= AV14GXV1 ) && ( AV14GXV1 > 0 ) )
               {
                  AV5authenticators.CurrentItem = ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV14GXV1));
                  AV10delete = cgiGet( edtavDelete_Internalname);
               }
            }
            if ( nGXsfl_9_fel_idx == 0 )
            {
               nGXsfl_9_idx = 1;
               sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
               SubsflControlProps_92( ) ;
            }
            nGXsfl_9_fel_idx = 1;
            /* Read variables values. */
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E14122 ();
         if (returnInSub) return;
      }

      protected void E14122( )
      {
         /* Start Routine */
         returnInSub = false;
         AV5authenticators.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "authenticators.auth", out  AV6error), null);
         gx_BV9 = true;
         AV10delete = "Delete";
         AssignAttri("", false, edtavDelete_Internalname, AV10delete);
      }

      protected void E12122( )
      {
         AV14GXV1 = nGXsfl_9_idx;
         if ( ( AV14GXV1 > 0 ) && ( AV5authenticators.Count >= AV14GXV1 ) )
         {
            AV5authenticators.CurrentItem = ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV14GXV1));
         }
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV7PopupAuthenticatorName = "Wallet.NewAuthenticator";
         AV9strFound = (short)(StringUtil.StringSearch( AV8PopupName, StringUtil.Lower( AV7PopupAuthenticatorName), 1));
         if ( AV9strFound > 1 )
         {
            AV5authenticators.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "authenticators.auth", out  AV6error), null);
            gx_BV9 = true;
         }
         /*  Sending Event outputs  */
         if ( gx_BV9 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV5authenticators", AV5authenticators);
            nGXsfl_9_bak_idx = nGXsfl_9_idx;
            gxgrGridauthenticators_refresh( AV10delete) ;
            nGXsfl_9_idx = nGXsfl_9_bak_idx;
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
      }

      protected void E15122( )
      {
         AV14GXV1 = nGXsfl_9_idx;
         if ( ( AV14GXV1 > 0 ) && ( AV5authenticators.Count >= AV14GXV1 ) )
         {
            AV5authenticators.CurrentItem = ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV14GXV1));
         }
         /* 'Get Pin' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.setauthenticator(context ).execute(  ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)(AV5authenticators.CurrentItem))) ;
         context.PopUp(formatLink("wallet.showauthenticatorpin") , new Object[] {});
      }

      protected void E16122( )
      {
         AV14GXV1 = nGXsfl_9_idx;
         if ( ( AV14GXV1 > 0 ) && ( AV5authenticators.Count >= AV14GXV1 ) )
         {
            AV5authenticators.CurrentItem = ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV14GXV1));
         }
         /* 'Delete Authenticator' Routine */
         returnInSub = false;
         this.executeExternalObjectMethod("", false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to Delete "+((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)(AV5authenticators.CurrentItem)).gxTpr_Accounttitle+" Authenticator?"}, false);
      }

      protected void E13122( )
      {
         AV14GXV1 = nGXsfl_9_idx;
         if ( ( AV14GXV1 > 0 ) && ( AV5authenticators.Count >= AV14GXV1 ) )
         {
            AV5authenticators.CurrentItem = ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV14GXV1));
         }
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV12UserResponse )
         {
            AV18GXV5 = 1;
            while ( AV18GXV5 <= AV5authenticators.Count )
            {
               AV13oneAuthenticator = ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV18GXV5));
               if ( StringUtil.StrCmp(StringUtil.Trim( AV13oneAuthenticator.gxTpr_Based32key), StringUtil.Trim( ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)(AV5authenticators.CurrentItem)).gxTpr_Based32key)) == 0 )
               {
                  AV5authenticators.RemoveItem(AV5authenticators.IndexOf(AV13oneAuthenticator));
                  gx_BV9 = true;
                  GXt_char1 = AV6error;
                  new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "authenticators.auth",  AV5authenticators.ToJSonString(false), out  GXt_char1) ;
                  AV6error = GXt_char1;
                  AssignAttri("", false, "AV6error", AV6error);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV6error)) )
                  {
                     AV5authenticators.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "authenticators.auth", out  AV6error), null);
                     gx_BV9 = true;
                  }
                  else
                  {
                     GX_msglist.addItem("Error saving encrypted file: "+AV6error);
                  }
               }
               AV18GXV5 = (int)(AV18GXV5+1);
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV5authenticators", AV5authenticators);
         nGXsfl_9_bak_idx = nGXsfl_9_idx;
         gxgrGridauthenticators_refresh( AV10delete) ;
         nGXsfl_9_idx = nGXsfl_9_bak_idx;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
      }

      private void E17122( )
      {
         /* Gridauthenticators_Load Routine */
         returnInSub = false;
         AV14GXV1 = 1;
         while ( AV14GXV1 <= AV5authenticators.Count )
         {
            AV5authenticators.CurrentItem = ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV14GXV1));
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 9;
            }
            sendrow_92( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_9_Refreshing )
            {
               DoAjaxLoad(9, GridauthenticatorsRow);
            }
            AV14GXV1 = (int)(AV14GXV1+1);
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
         PA122( ) ;
         WS122( ) ;
         WE122( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20255213173959", true, true);
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
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true);
         context.AddJavascriptSource("wallet/autheticators.js", "?20255213173960", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_92( )
      {
         edtavCtlissuer_Internalname = "CTLISSUER_"+sGXsfl_9_idx;
         edtavCtlaccounttitle_Internalname = "CTLACCOUNTTITLE_"+sGXsfl_9_idx;
         edtavCtlaccounttitle1_Internalname = "CTLACCOUNTTITLE1_"+sGXsfl_9_idx;
         edtavDelete_Internalname = "vDELETE_"+sGXsfl_9_idx;
      }

      protected void SubsflControlProps_fel_92( )
      {
         edtavCtlissuer_Internalname = "CTLISSUER_"+sGXsfl_9_fel_idx;
         edtavCtlaccounttitle_Internalname = "CTLACCOUNTTITLE_"+sGXsfl_9_fel_idx;
         edtavCtlaccounttitle1_Internalname = "CTLACCOUNTTITLE1_"+sGXsfl_9_fel_idx;
         edtavDelete_Internalname = "vDELETE_"+sGXsfl_9_fel_idx;
      }

      protected void sendrow_92( )
      {
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         WB120( ) ;
         GridauthenticatorsRow = GXWebRow.GetNew(context,GridauthenticatorsContainer);
         if ( subGridauthenticators_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridauthenticators_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridauthenticators_Class, "") != 0 )
            {
               subGridauthenticators_Linesclass = subGridauthenticators_Class+"Odd";
            }
         }
         else if ( subGridauthenticators_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridauthenticators_Backstyle = 0;
            subGridauthenticators_Backcolor = subGridauthenticators_Allbackcolor;
            if ( StringUtil.StrCmp(subGridauthenticators_Class, "") != 0 )
            {
               subGridauthenticators_Linesclass = subGridauthenticators_Class+"Uniform";
            }
         }
         else if ( subGridauthenticators_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridauthenticators_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridauthenticators_Class, "") != 0 )
            {
               subGridauthenticators_Linesclass = subGridauthenticators_Class+"Odd";
            }
            subGridauthenticators_Backcolor = (int)(0x0);
         }
         else if ( subGridauthenticators_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridauthenticators_Backstyle = 1;
            if ( ((int)((nGXsfl_9_idx) % (2))) == 0 )
            {
               subGridauthenticators_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridauthenticators_Class, "") != 0 )
               {
                  subGridauthenticators_Linesclass = subGridauthenticators_Class+"Even";
               }
            }
            else
            {
               subGridauthenticators_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridauthenticators_Class, "") != 0 )
               {
                  subGridauthenticators_Linesclass = subGridauthenticators_Class+"Odd";
               }
            }
         }
         if ( GridauthenticatorsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_9_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridauthenticatorsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridauthenticatorsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlissuer_Internalname,StringUtil.RTrim( ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV14GXV1)).gxTpr_Issuer),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,10);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlissuer_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlissuer_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)60,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridauthenticatorsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridauthenticatorsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlaccounttitle_Internalname,StringUtil.RTrim( ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV14GXV1)).gxTpr_Accounttitle),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,11);\"","'"+""+"'"+",false,"+"'"+"E\\'GET PIN\\'."+sGXsfl_9_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlaccounttitle_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlaccounttitle_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)60,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridauthenticatorsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridauthenticatorsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlaccounttitle1_Internalname,context.localUtil.TToC( ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV14GXV1)).gxTpr_Createddatetime, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator)AV5authenticators.Item(AV14GXV1)).gxTpr_Createddatetime, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,12);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlaccounttitle1_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlaccounttitle1_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridauthenticatorsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'" + sGXsfl_9_idx + "',9)\"";
         ROClassString = "Attribute";
         GridauthenticatorsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDelete_Internalname,StringUtil.RTrim( AV10delete),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,13);\"","'"+""+"'"+",false,"+"'"+"E\\'DELETE AUTHENTICATOR\\'."+sGXsfl_9_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDelete_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavDelete_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)9,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         send_integrity_lvl_hashes122( ) ;
         GridauthenticatorsContainer.AddRow(GridauthenticatorsRow);
         nGXsfl_9_idx = ((subGridauthenticators_Islastpage==1)&&(nGXsfl_9_idx+1>subGridauthenticators_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         /* End function sendrow_92 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl9( )
      {
         if ( GridauthenticatorsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridauthenticatorsContainer"+"DivS\" data-gxgridid=\"9\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridauthenticators_Internalname, subGridauthenticators_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridauthenticators_Backcolorstyle == 0 )
            {
               subGridauthenticators_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridauthenticators_Class) > 0 )
               {
                  subGridauthenticators_Linesclass = subGridauthenticators_Class+"Title";
               }
            }
            else
            {
               subGridauthenticators_Titlebackstyle = 1;
               if ( subGridauthenticators_Backcolorstyle == 1 )
               {
                  subGridauthenticators_Titlebackcolor = subGridauthenticators_Allbackcolor;
                  if ( StringUtil.Len( subGridauthenticators_Class) > 0 )
                  {
                     subGridauthenticators_Linesclass = subGridauthenticators_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridauthenticators_Class) > 0 )
                  {
                     subGridauthenticators_Linesclass = subGridauthenticators_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Issuer") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Account Title") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Created") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridauthenticatorsContainer.AddObjectProperty("GridName", "Gridauthenticators");
         }
         else
         {
            GridauthenticatorsContainer.AddObjectProperty("GridName", "Gridauthenticators");
            GridauthenticatorsContainer.AddObjectProperty("Header", subGridauthenticators_Header);
            GridauthenticatorsContainer.AddObjectProperty("Class", "Grid");
            GridauthenticatorsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridauthenticatorsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridauthenticatorsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthenticators_Backcolorstyle), 1, 0, ".", "")));
            GridauthenticatorsContainer.AddObjectProperty("CmpContext", "");
            GridauthenticatorsContainer.AddObjectProperty("InMasterPage", "false");
            GridauthenticatorsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthenticatorsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlissuer_Enabled), 5, 0, ".", "")));
            GridauthenticatorsContainer.AddColumnProperties(GridauthenticatorsColumn);
            GridauthenticatorsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthenticatorsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlaccounttitle_Enabled), 5, 0, ".", "")));
            GridauthenticatorsContainer.AddColumnProperties(GridauthenticatorsColumn);
            GridauthenticatorsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthenticatorsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlaccounttitle1_Enabled), 5, 0, ".", "")));
            GridauthenticatorsContainer.AddColumnProperties(GridauthenticatorsColumn);
            GridauthenticatorsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthenticatorsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV10delete)));
            GridauthenticatorsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDelete_Enabled), 5, 0, ".", "")));
            GridauthenticatorsContainer.AddColumnProperties(GridauthenticatorsColumn);
            GridauthenticatorsContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthenticators_Selectedindex), 4, 0, ".", "")));
            GridauthenticatorsContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthenticators_Allowselection), 1, 0, ".", "")));
            GridauthenticatorsContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthenticators_Selectioncolor), 9, 0, ".", "")));
            GridauthenticatorsContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthenticators_Allowhovering), 1, 0, ".", "")));
            GridauthenticatorsContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthenticators_Hoveringcolor), 9, 0, ".", "")));
            GridauthenticatorsContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthenticators_Allowcollapsing), 1, 0, ".", "")));
            GridauthenticatorsContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthenticators_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         bttImportanewauthenticatorcode_Internalname = "IMPORTANEWAUTHENTICATORCODE";
         edtavCtlissuer_Internalname = "CTLISSUER";
         edtavCtlaccounttitle_Internalname = "CTLACCOUNTTITLE";
         edtavCtlaccounttitle1_Internalname = "CTLACCOUNTTITLE1";
         edtavDelete_Internalname = "vDELETE";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subGridauthenticators_Internalname = "GRIDAUTHENTICATORS";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridauthenticators_Allowcollapsing = 0;
         subGridauthenticators_Allowselection = 0;
         subGridauthenticators_Header = "";
         edtavDelete_Jsonclick = "";
         edtavDelete_Enabled = 1;
         edtavCtlaccounttitle1_Jsonclick = "";
         edtavCtlaccounttitle1_Enabled = 0;
         edtavCtlaccounttitle_Jsonclick = "";
         edtavCtlaccounttitle_Enabled = 0;
         edtavCtlissuer_Jsonclick = "";
         edtavCtlissuer_Enabled = 0;
         subGridauthenticators_Class = "Grid";
         subGridauthenticators_Backcolorstyle = 0;
         edtavCtlaccounttitle1_Enabled = -1;
         edtavCtlaccounttitle_Enabled = -1;
         edtavCtlissuer_Enabled = -1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Autheticators";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDAUTHENTICATORS_nFirstRecordOnPage","type":"int"},{"av":"GRIDAUTHENTICATORS_nEOF","type":"int"},{"av":"AV5authenticators","fld":"vAUTHENTICATORS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"nRC_GXsfl_9","ctrl":"GRIDAUTHENTICATORS","prop":"GridRC","grid":9,"type":"int"},{"av":"AV10delete","fld":"vDELETE","type":"char"}]}""");
         setEventMetadata("'IMPORT A NEW AUTHENTICATOR CODE'","""{"handler":"E11121","iparms":[]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E12122","iparms":[{"av":"AV8PopupName","fld":"vPOPUPNAME","type":"char"},{"av":"AV6error","fld":"vERROR","type":"char"},{"av":"AV5authenticators","fld":"vAUTHENTICATORS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDAUTHENTICATORS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDAUTHENTICATORS","prop":"GridRC","grid":9,"type":"int"},{"av":"GRIDAUTHENTICATORS_nEOF","type":"int"},{"av":"AV10delete","fld":"vDELETE","type":"char"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"AV5authenticators","fld":"vAUTHENTICATORS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDAUTHENTICATORS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDAUTHENTICATORS","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("'GET PIN'","""{"handler":"E15122","iparms":[{"av":"AV5authenticators","fld":"vAUTHENTICATORS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDAUTHENTICATORS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDAUTHENTICATORS","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("'DELETE AUTHENTICATOR'","""{"handler":"E16122","iparms":[{"av":"AV5authenticators","fld":"vAUTHENTICATORS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDAUTHENTICATORS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDAUTHENTICATORS","prop":"GridRC","grid":9,"type":"int"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E13122","iparms":[{"av":"AV12UserResponse","fld":"vUSERRESPONSE","type":"boolean"},{"av":"AV5authenticators","fld":"vAUTHENTICATORS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDAUTHENTICATORS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDAUTHENTICATORS","prop":"GridRC","grid":9,"type":"int"},{"av":"GRIDAUTHENTICATORS_nEOF","type":"int"},{"av":"AV10delete","fld":"vDELETE","type":"char"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED",""","oparms":[{"av":"AV5authenticators","fld":"vAUTHENTICATORS","grid":9,"type":""},{"av":"nGXsfl_9_idx","ctrl":"GRID","prop":"GridCurrRow","grid":9},{"av":"GRIDAUTHENTICATORS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_9","ctrl":"GRIDAUTHENTICATORS","prop":"GridRC","grid":9,"type":"int"},{"av":"AV6error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Delete","iparms":[]}""");
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
         AV10delete = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV5authenticators = new GXBaseCollection<GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator>( context, "Authenticator", "distributedcryptography");
         AV8PopupName = "";
         AV6error = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttImportanewauthenticatorcode_Jsonclick = "";
         GridauthenticatorsContainer = new GXWebGrid( context);
         sStyleString = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV7PopupAuthenticatorName = "";
         AV13oneAuthenticator = new GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator(context);
         GXt_char1 = "";
         GridauthenticatorsRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridauthenticators_Linesclass = "";
         ROClassString = "";
         GridauthenticatorsColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlissuer_Enabled = 0;
         edtavCtlaccounttitle_Enabled = 0;
         edtavCtlaccounttitle1_Enabled = 0;
         edtavDelete_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridauthenticators_Backcolorstyle ;
      private short AV9strFound ;
      private short GRIDAUTHENTICATORS_nEOF ;
      private short nGXWrapped ;
      private short subGridauthenticators_Backstyle ;
      private short subGridauthenticators_Titlebackstyle ;
      private short subGridauthenticators_Allowselection ;
      private short subGridauthenticators_Allowhovering ;
      private short subGridauthenticators_Allowcollapsing ;
      private short subGridauthenticators_Collapsed ;
      private int nRC_GXsfl_9 ;
      private int nGXsfl_9_idx=1 ;
      private int AV14GXV1 ;
      private int subGridauthenticators_Islastpage ;
      private int edtavCtlissuer_Enabled ;
      private int edtavCtlaccounttitle_Enabled ;
      private int edtavCtlaccounttitle1_Enabled ;
      private int edtavDelete_Enabled ;
      private int nGXsfl_9_fel_idx=1 ;
      private int nGXsfl_9_bak_idx=1 ;
      private int AV18GXV5 ;
      private int idxLst ;
      private int subGridauthenticators_Backcolor ;
      private int subGridauthenticators_Allbackcolor ;
      private int subGridauthenticators_Titlebackcolor ;
      private int subGridauthenticators_Selectedindex ;
      private int subGridauthenticators_Selectioncolor ;
      private int subGridauthenticators_Hoveringcolor ;
      private long GRIDAUTHENTICATORS_nCurrentRecord ;
      private long GRIDAUTHENTICATORS_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_9_idx="0001" ;
      private string AV10delete ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV8PopupName ;
      private string AV6error ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttImportanewauthenticatorcode_Internalname ;
      private string bttImportanewauthenticatorcode_Jsonclick ;
      private string sStyleString ;
      private string subGridauthenticators_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavDelete_Internalname ;
      private string sGXsfl_9_fel_idx="0001" ;
      private string AV7PopupAuthenticatorName ;
      private string GXt_char1 ;
      private string edtavCtlissuer_Internalname ;
      private string edtavCtlaccounttitle_Internalname ;
      private string edtavCtlaccounttitle1_Internalname ;
      private string subGridauthenticators_Class ;
      private string subGridauthenticators_Linesclass ;
      private string ROClassString ;
      private string edtavCtlissuer_Jsonclick ;
      private string edtavCtlaccounttitle_Jsonclick ;
      private string edtavCtlaccounttitle1_Jsonclick ;
      private string edtavDelete_Jsonclick ;
      private string subGridauthenticators_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV12UserResponse ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_9_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV9 ;
      private GXWebGrid GridauthenticatorsContainer ;
      private GXWebRow GridauthenticatorsRow ;
      private GXWebColumn GridauthenticatorsColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator> AV5authenticators ;
      private GeneXus.Programs.googleauthenticator.SdtSDT_Authenticators_Authenticator AV13oneAuthenticator ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
