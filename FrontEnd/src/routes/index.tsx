import { createFileRoute } from '@tanstack/react-router'
import TaskListForm from '../components/TaskListForm'

export const Route = createFileRoute('/')({
  component: App,
})

function App() {
  const handleTaskListCreated = () => {
    alert('Task list created successfully!');
  };

  return (
    <div className="App">
      <TaskListForm onTaskListCreated={handleTaskListCreated} />
    </div>
  )
}
