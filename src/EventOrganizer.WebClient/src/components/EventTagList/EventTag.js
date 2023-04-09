import { Chip } from '@mui/material'

export default function EventTag({ tag, onClick, onDelete }) {

    return tag && (
        <Chip
            sx={{ margin: '1px' }}
            size='small'
            label={`#${tag}`}
            onClick={onClick}
            onDelete={onDelete}
        />
    )
}