import { useSelector } from 'react-redux'
import { Link } from 'react-router-dom'
import SignInControl from './SignInControl'
import NavMenu from '../NavMenu/NavMenu'
import routes from '../../constants/route-constants'

export default function Header () {

    const isUserSignedIn = useSelector((state) => state.auth.isUserSignedIn)

    return (
        <header>
            <Link to={routes.root}>
                <img src='/logo192.png' alt='Welcome Page'/>
                <h2>Event Organizer</h2>
            </Link>
            { isUserSignedIn && <NavMenu /> }
            <SignInControl />
        </header>
    )
}