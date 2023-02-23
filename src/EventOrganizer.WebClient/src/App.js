import { useSelector } from 'react-redux'
import { createBrowserRouter, RouterProvider, Outlet } from "react-router-dom";
import Header from './components/Header/Header';
import Footer from './components/Footer/Footer';
import ErrorPage from './components/ErrorPage/ErrorPage';
import StartPage from './components/StartPage/StartPage';
import AllEventsPage from './components/AllEventsPage/AllEventsPage';
import OwnEventsPage from './components/OwnEventsPage/OwnEventsPage';
import CalendarPage from './components/CalendarPage/CalendarPage';
import SpecificEventPage, {
    loader as eventLoader,
} from './components/SpecificEventPage/SpecificEventPage';
import LoadingIndicator from './components/LoadingIndicator/LoadingIndicator'
import routes from './constants/route-constants';

export default function App () {

    const isUserSignedIn = useSelector((state) => state.auth.isUserSignedIn)

    const appElement = () => (
        <div>
            <Header />
            <Outlet />
            <Footer />
            <LoadingIndicator />
        </div>
    )

    let appPages = [
        {
            path: routes.root,
            element: <StartPage />,
        }
    ]

    if(isUserSignedIn){
        appPages.push(
            {
                path: routes.events,
                element: <AllEventsPage />,
            },
            {
                path: `${routes.events}/:id`,
                element: <SpecificEventPage />,
                loader: eventLoader,
            },
            {
                path: routes.ownEvents,
                element: <OwnEventsPage />,
            },
            {
                path: routes.calendar,
                element: <CalendarPage />,
            }
        )
    }

    const router = createBrowserRouter([
        {
            path: routes.root,
            element: appElement(),
            errorElement: <ErrorPage />,
            children: appPages
        }
    ]);

    return <RouterProvider router={router} />
}
