import { useSelector } from 'react-redux'
import { signIn, signOut } from '../../store/authSlice'
import { Link } from 'react-router-dom'
import { signinRedirect, signoutRedirect } from '../../services/userService'
import routes from '../../constants/route-constants'

export default function SignInControl () {
    
    const isUserSignedIn = useSelector((state) => state.auth.user)

    const onClick = () => {
        isUserSignedIn 
            ? signoutRedirect()
            : signinRedirect();
    }

    return (
        <Link
            to={routes.root}
            onClick={onClick} >
                <label>{isUserSignedIn ? "Sign Out" : "Sign In"}</label>
        </Link>
    )
}