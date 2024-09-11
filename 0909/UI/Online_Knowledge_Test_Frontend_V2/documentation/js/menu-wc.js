'use strict';

customElements.define('compodoc-menu', class extends HTMLElement {
    constructor() {
        super();
        this.isNormalMode = this.getAttribute('mode') === 'normal';
    }

    connectedCallback() {
        this.render(this.isNormalMode);
    }

    render(isNormalMode) {
        let tp = lithtml.html(`
        <nav>
            <ul class="list">
                <li class="title">
                    <a href="index.html" data-type="index-link">online-knowledge-test-v2 documentation</a>
                </li>

                <li class="divider"></li>
                ${ isNormalMode ? `<div id="book-search-input" role="search"><input type="text" placeholder="Type to search"></div>` : '' }
                <li class="chapter">
                    <a data-type="chapter-link" href="index.html"><span class="icon ion-ios-home"></span>Getting started</a>
                    <ul class="links">
                        <li class="link">
                            <a href="overview.html" data-type="chapter-link">
                                <span class="icon ion-ios-keypad"></span>Overview
                            </a>
                        </li>
                        <li class="link">
                            <a href="index.html" data-type="chapter-link">
                                <span class="icon ion-ios-paper"></span>README
                            </a>
                        </li>
                                <li class="link">
                                    <a href="dependencies.html" data-type="chapter-link">
                                        <span class="icon ion-ios-list"></span>Dependencies
                                    </a>
                                </li>
                                <li class="link">
                                    <a href="properties.html" data-type="chapter-link">
                                        <span class="icon ion-ios-apps"></span>Properties
                                    </a>
                                </li>
                    </ul>
                </li>
                    <li class="chapter modules">
                        <a data-type="chapter-link" href="modules.html">
                            <div class="menu-toggler linked" data-bs-toggle="collapse" ${ isNormalMode ?
                                'data-bs-target="#modules-links"' : 'data-bs-target="#xs-modules-links"' }>
                                <span class="icon ion-ios-archive"></span>
                                <span class="link-name">Modules</span>
                                <span class="icon ion-ios-arrow-down"></span>
                            </div>
                        </a>
                        <ul class="links collapse " ${ isNormalMode ? 'id="modules-links"' : 'id="xs-modules-links"' }>
                            <li class="link">
                                <a href="modules/AdminModule.html" data-type="entity-link" >AdminModule</a>
                                    <li class="chapter inner">
                                        <div class="simple menu-toggler" data-bs-toggle="collapse" ${ isNormalMode ?
                                            'data-bs-target="#components-links-module-AdminModule-01ac44004a365a38637971aa7d8fb77e81ccfa733c486d0562d81a2c539f5ca41cd3fc46a1c6faffd6176db9d5080814d00438f71b09b75176bee9fd9ef1199a"' : 'data-bs-target="#xs-components-links-module-AdminModule-01ac44004a365a38637971aa7d8fb77e81ccfa733c486d0562d81a2c539f5ca41cd3fc46a1c6faffd6176db9d5080814d00438f71b09b75176bee9fd9ef1199a"' }>
                                            <span class="icon ion-md-cog"></span>
                                            <span>Components</span>
                                            <span class="icon ion-ios-arrow-down"></span>
                                        </div>
                                        <ul class="links collapse" ${ isNormalMode ? 'id="components-links-module-AdminModule-01ac44004a365a38637971aa7d8fb77e81ccfa733c486d0562d81a2c539f5ca41cd3fc46a1c6faffd6176db9d5080814d00438f71b09b75176bee9fd9ef1199a"' :
                                            'id="xs-components-links-module-AdminModule-01ac44004a365a38637971aa7d8fb77e81ccfa733c486d0562d81a2c539f5ca41cd3fc46a1c6faffd6176db9d5080814d00438f71b09b75176bee9fd9ef1199a"' }>
                                            <li class="link">
                                                <a href="components/FeedbackDashboardComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >FeedbackDashboardComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/FooterComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >FooterComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/HomeComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >HomeComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/SidebarComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >SidebarComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/TestCreateComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >TestCreateComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/TestHistoryDashboardComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >TestHistoryDashboardComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/UsersDashboardComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >UsersDashboardComponent</a>
                                            </li>
                                        </ul>
                                    </li>
                            </li>
                            <li class="link">
                                <a href="modules/AdminRoutingModule.html" data-type="entity-link" >AdminRoutingModule</a>
                            </li>
                            <li class="link">
                                <a href="modules/AppModule.html" data-type="entity-link" >AppModule</a>
                                    <li class="chapter inner">
                                        <div class="simple menu-toggler" data-bs-toggle="collapse" ${ isNormalMode ?
                                            'data-bs-target="#components-links-module-AppModule-de875ed45761a36ba11f216b3099bc94bc71ca7696cb76e27b4e121cad007532e6355f0486d0ba9aa13ef58cce5be6c0cfee9a2a038d645a8cfb7f3b31d614f1"' : 'data-bs-target="#xs-components-links-module-AppModule-de875ed45761a36ba11f216b3099bc94bc71ca7696cb76e27b4e121cad007532e6355f0486d0ba9aa13ef58cce5be6c0cfee9a2a038d645a8cfb7f3b31d614f1"' }>
                                            <span class="icon ion-md-cog"></span>
                                            <span>Components</span>
                                            <span class="icon ion-ios-arrow-down"></span>
                                        </div>
                                        <ul class="links collapse" ${ isNormalMode ? 'id="components-links-module-AppModule-de875ed45761a36ba11f216b3099bc94bc71ca7696cb76e27b4e121cad007532e6355f0486d0ba9aa13ef58cce5be6c0cfee9a2a038d645a8cfb7f3b31d614f1"' :
                                            'id="xs-components-links-module-AppModule-de875ed45761a36ba11f216b3099bc94bc71ca7696cb76e27b4e121cad007532e6355f0486d0ba9aa13ef58cce5be6c0cfee9a2a038d645a8cfb7f3b31d614f1"' }>
                                            <li class="link">
                                                <a href="components/AppComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >AppComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/FooterComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >FooterComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/LandingPageComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >LandingPageComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/LoginComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >LoginComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/NavbarComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >NavbarComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/PageNotFoundComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >PageNotFoundComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/SignupComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >SignupComponent</a>
                                            </li>
                                        </ul>
                                    </li>
                            </li>
                            <li class="link">
                                <a href="modules/AppRoutingModule.html" data-type="entity-link" >AppRoutingModule</a>
                            </li>
                            <li class="link">
                                <a href="modules/UserModule.html" data-type="entity-link" >UserModule</a>
                                    <li class="chapter inner">
                                        <div class="simple menu-toggler" data-bs-toggle="collapse" ${ isNormalMode ?
                                            'data-bs-target="#components-links-module-UserModule-5f1cb50374ef43776b1947a6aedb1342dbe7df0a41cda16f3113d225b4dbef45d1bc565b818d6710d6e7e9d94dad2f9cec3fd4358463ab9fd02294931ebbd7a9"' : 'data-bs-target="#xs-components-links-module-UserModule-5f1cb50374ef43776b1947a6aedb1342dbe7df0a41cda16f3113d225b4dbef45d1bc565b818d6710d6e7e9d94dad2f9cec3fd4358463ab9fd02294931ebbd7a9"' }>
                                            <span class="icon ion-md-cog"></span>
                                            <span>Components</span>
                                            <span class="icon ion-ios-arrow-down"></span>
                                        </div>
                                        <ul class="links collapse" ${ isNormalMode ? 'id="components-links-module-UserModule-5f1cb50374ef43776b1947a6aedb1342dbe7df0a41cda16f3113d225b4dbef45d1bc565b818d6710d6e7e9d94dad2f9cec3fd4358463ab9fd02294931ebbd7a9"' :
                                            'id="xs-components-links-module-UserModule-5f1cb50374ef43776b1947a6aedb1342dbe7df0a41cda16f3113d225b4dbef45d1bc565b818d6710d6e7e9d94dad2f9cec3fd4358463ab9fd02294931ebbd7a9"' }>
                                            <li class="link">
                                                <a href="components/ChooseTestComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >ChooseTestComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/FeedbackComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >FeedbackComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/FooterComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >FooterComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/ProfileComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >ProfileComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/ResultComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >ResultComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/TestComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >TestComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/TestHistoryComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >TestHistoryComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/TestInstructionComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules" >TestInstructionComponent</a>
                                            </li>
                                        </ul>
                                    </li>
                            </li>
                            <li class="link">
                                <a href="modules/UserRoutingModule.html" data-type="entity-link" >UserRoutingModule</a>
                            </li>
                </ul>
                </li>
                    <li class="chapter">
                        <div class="simple menu-toggler" data-bs-toggle="collapse" ${ isNormalMode ? 'data-bs-target="#components-links"' :
                            'data-bs-target="#xs-components-links"' }>
                            <span class="icon ion-md-cog"></span>
                            <span>Components</span>
                            <span class="icon ion-ios-arrow-down"></span>
                        </div>
                        <ul class="links collapse " ${ isNormalMode ? 'id="components-links"' : 'id="xs-components-links"' }>
                            <li class="link">
                                <a href="components/FooterComponent-1.html" data-type="entity-link" >FooterComponent</a>
                            </li>
                            <li class="link">
                                <a href="components/FooterComponent-2.html" data-type="entity-link" >FooterComponent</a>
                            </li>
                            <li class="link">
                                <a href="components/HomeComponent-1.html" data-type="entity-link" >HomeComponent</a>
                            </li>
                            <li class="link">
                                <a href="components/NavbarComponent-1.html" data-type="entity-link" >NavbarComponent</a>
                            </li>
                            <li class="link">
                                <a href="components/NavbarComponent-2.html" data-type="entity-link" >NavbarComponent</a>
                            </li>
                        </ul>
                    </li>
                        <li class="chapter">
                            <div class="simple menu-toggler" data-bs-toggle="collapse" ${ isNormalMode ? 'data-bs-target="#injectables-links"' :
                                'data-bs-target="#xs-injectables-links"' }>
                                <span class="icon ion-md-arrow-round-down"></span>
                                <span>Injectables</span>
                                <span class="icon ion-ios-arrow-down"></span>
                            </div>
                            <ul class="links collapse " ${ isNormalMode ? 'id="injectables-links"' : 'id="xs-injectables-links"' }>
                                <li class="link">
                                    <a href="injectables/ApiService.html" data-type="entity-link" >ApiService</a>
                                </li>
                                <li class="link">
                                    <a href="injectables/AuthService.html" data-type="entity-link" >AuthService</a>
                                </li>
                            </ul>
                        </li>
                    <li class="chapter">
                        <div class="simple menu-toggler" data-bs-toggle="collapse" ${ isNormalMode ? 'data-bs-target="#interceptors-links"' :
                            'data-bs-target="#xs-interceptors-links"' }>
                            <span class="icon ion-ios-swap"></span>
                            <span>Interceptors</span>
                            <span class="icon ion-ios-arrow-down"></span>
                        </div>
                        <ul class="links collapse " ${ isNormalMode ? 'id="interceptors-links"' : 'id="xs-interceptors-links"' }>
                            <li class="link">
                                <a href="interceptors/AuthInterceptor.html" data-type="entity-link" >AuthInterceptor</a>
                            </li>
                        </ul>
                    </li>
                    <li class="chapter">
                        <div class="simple menu-toggler" data-bs-toggle="collapse" ${ isNormalMode ? 'data-bs-target="#interfaces-links"' :
                            'data-bs-target="#xs-interfaces-links"' }>
                            <span class="icon ion-md-information-circle-outline"></span>
                            <span>Interfaces</span>
                            <span class="icon ion-ios-arrow-down"></span>
                        </div>
                        <ul class="links collapse " ${ isNormalMode ? ' id="interfaces-links"' : 'id="xs-interfaces-links"' }>
                            <li class="link">
                                <a href="interfaces/IExam.html" data-type="entity-link" >IExam</a>
                            </li>
                            <li class="link">
                                <a href="interfaces/IExamLive.html" data-type="entity-link" >IExamLive</a>
                            </li>
                            <li class="link">
                                <a href="interfaces/ILoggedInUser.html" data-type="entity-link" >ILoggedInUser</a>
                            </li>
                            <li class="link">
                                <a href="interfaces/ILoginForm.html" data-type="entity-link" >ILoginForm</a>
                            </li>
                            <li class="link">
                                <a href="interfaces/IOption.html" data-type="entity-link" >IOption</a>
                            </li>
                            <li class="link">
                                <a href="interfaces/IQuestion.html" data-type="entity-link" >IQuestion</a>
                            </li>
                            <li class="link">
                                <a href="interfaces/ISection.html" data-type="entity-link" >ISection</a>
                            </li>
                            <li class="link">
                                <a href="interfaces/ISignupForm.html" data-type="entity-link" >ISignupForm</a>
                            </li>
                            <li class="link">
                                <a href="interfaces/User.html" data-type="entity-link" >User</a>
                            </li>
                        </ul>
                    </li>
                    <li class="chapter">
                        <div class="simple menu-toggler" data-bs-toggle="collapse" ${ isNormalMode ? 'data-bs-target="#miscellaneous-links"'
                            : 'data-bs-target="#xs-miscellaneous-links"' }>
                            <span class="icon ion-ios-cube"></span>
                            <span>Miscellaneous</span>
                            <span class="icon ion-ios-arrow-down"></span>
                        </div>
                        <ul class="links collapse " ${ isNormalMode ? 'id="miscellaneous-links"' : 'id="xs-miscellaneous-links"' }>
                            <li class="link">
                                <a href="miscellaneous/variables.html" data-type="entity-link">Variables</a>
                            </li>
                        </ul>
                    </li>
                        <li class="chapter">
                            <a data-type="chapter-link" href="routes.html"><span class="icon ion-ios-git-branch"></span>Routes</a>
                        </li>
                    <li class="chapter">
                        <a data-type="chapter-link" href="coverage.html"><span class="icon ion-ios-stats"></span>Documentation coverage</a>
                    </li>
                    <li class="divider"></li>
                    <li class="copyright">
                        Documentation generated using <a href="https://compodoc.app/" target="_blank" rel="noopener noreferrer">
                            <img data-src="images/compodoc-vectorise.png" class="img-responsive" data-type="compodoc-logo">
                        </a>
                    </li>
            </ul>
        </nav>
        `);
        this.innerHTML = tp.strings;
    }
});