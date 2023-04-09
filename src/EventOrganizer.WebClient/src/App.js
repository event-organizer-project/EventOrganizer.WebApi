import React, { useEffect } from 'react';
import { Provider } from 'react-redux';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import store from './store/store';
import authService from './services/authService'
import routes from './constants/route-constants';

import AuthProvider from './components/Auth/authProvider'
import PrivateRoute from './components/Auth/protectedRoute'
import SigninOidc from './components/Auth/signin-oidc'
import SignoutOidc from './components/Auth/signout-oidc'
import Header from './components/Header/Header';
import Footer from './components/Footer/Footer';
import StartPage from './components/StartPage/StartPage';
import AllEventsPage from './components/AllEventsPage/AllEventsPage';
import OwnEventsPage from './components/OwnEventsPage/OwnEventsPage';
import CalendarPage from './components/CalendarPage/CalendarPage';
import SpecificEventPage from './components/SpecificEventPage/SpecificEventPage';
import LoadingIndicator from './components/LoadingIndicator/LoadingIndicator';

export default function App() {

  useEffect(() => {
    authService.loadUserFromStorage(store)
  }, [])

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Provider store={store}>
        <AuthProvider userManager={authService.getUserManager()} store={store}>
          <Router>
            <Header />
            <main>
              <Switch>
                <Route path={routes.signin} component={SigninOidc} />
                <Route path={routes.signout} component={SignoutOidc} />
                <Route exact path={routes.root} component={StartPage} />
                <PrivateRoute path={`${routes.events}/:id`} component={SpecificEventPage} />
                <PrivateRoute path={routes.events} component={AllEventsPage} />
                <PrivateRoute path={routes.ownEvents} component={OwnEventsPage} />
                <PrivateRoute path={routes.calendar} component={CalendarPage} />
              </Switch>
            </main>
            <Footer />
            <LoadingIndicator />
          </Router>
        </AuthProvider>
      </Provider>
    </LocalizationProvider>
  )
}
