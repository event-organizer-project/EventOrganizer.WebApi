import { Link } from 'react-router-dom'
import routes from '../../constants/route-constants'

export default function NavMenu () {
    return (
        <nav>
            <Link to={routes.events}>All Events</Link>
            <Link to={routes.ownEvents}>Own Events</Link>
            <Link to={routes.calendar}>Calendar</Link>
        </nav>
    )
}