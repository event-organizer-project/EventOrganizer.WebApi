import { Stack } from '@mui/material'
import EventTag from './EventTag'

export default function EventTagList ({ tags, onClick, onDelete }) {

    return tags && (
        <Stack direction="row" sx={{ flexWrap: 'wrap' }}>
            {tags.map(tag => (
                <EventTag key={tag} tag={tag} onClick={onClick} onDelete={onDelete} />
            ))}
        </Stack>
    )
}