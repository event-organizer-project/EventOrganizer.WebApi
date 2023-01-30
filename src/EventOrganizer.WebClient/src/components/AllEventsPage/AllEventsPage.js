import { Link } from 'react-router-dom'
import routes from '../../constants/route-constants'

export default function AllEventsPage () {
    return (
        <main>
            <h3>There will be a page with an event search panel and tools for creating new events.</h3>
            <Link to={`/${routes.events}/1`}>
                <label>To event with id 1</label>
            </Link>
        </main>
    )
}