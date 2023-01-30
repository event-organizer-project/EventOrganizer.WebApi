import { useSelector, useDispatch } from 'react-redux'
import { signIn, signOut } from '../../store/authSlice'
import { Link } from 'react-router-dom'
import routes from '../../constants/route-constants'

export default function SignInControl () {
    
    const isUserSignedIn = useSelector((state) => state.auth.isUserSignedIn)

    const dispatch = useDispatch()

    const onClick = () => {
        isUserSignedIn 
            ? dispatch(signOut())
            : dispatch(signIn()); 
    }

    return (
        <Link
            to={routes.root}
            onClick={onClick} >
                <label>{isUserSignedIn ? "Sign Out" : "Sign In"}</label>
        </Link>
    )
}