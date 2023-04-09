import { useSelector } from 'react-redux'
import { Link } from 'react-router-dom'
import authService from '../../services/authService'
import routes from '../../constants/route-constants'

export default function SignInControl() {

    const isUserSignedIn = useSelector((state) => state.auth.user)
    const user = useSelector((state) => state.auth.user)

    const onClick = () => {
        isUserSignedIn
            ? authService.signoutRedirect()
            : authService.signinRedirect();
    }

    const getUser = (e) => {
        e.preventDefault();
        console.log(user)
    }

    return (
        <div>
            {isUserSignedIn &&
            <Link
                to='#'
                onClick={getUser} >
                <label>{"User"}</label>
            </Link> }
            <Link
                to={routes.root}
                onClick={onClick} >
                <label>{isUserSignedIn ? "Sign Out" : "Sign In"}</label>
            </Link>
        </div>
    )
}